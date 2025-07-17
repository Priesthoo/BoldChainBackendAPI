using AutoMapper;
using BoldChainBackendAPI.BoldChainInterface;
using BoldChainBackendAPI.BoldChainModel.BoldChainDto;
using BoldChainBackendAPI.BoldChainModel.BoldChainEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using BoldChainBackendAPI.BoldChainException;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BoldChainBackendAPI.BoldChainService
{
    public class UserService : IUserService
    {
        private readonly UserManager< User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _jwtService;
        private readonly IWalletService _walletService;
        private readonly IKeyService _keyService;
        private readonly ILogger<UserService> _logger;  
        
        private readonly IBoldIdentity _identity;
        private readonly IIdentityRegistryService _identityRegistryService;

        public UserService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService jwtService,
            IWalletService walletService,
            IKeyService keyService,
            IBoldIdentity identity,
            IIdentityRegistryService identityRegistryService,
            ILogger<UserService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _walletService = walletService;
            _keyService = keyService;
            _identity = identity;
            _identityRegistryService=identityRegistryService;
            _logger=logger;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null) throw new Exception("Email already in use");
            ValidatiionHelper.ValidatePassword(dto.Password);
            ValidatiionHelper.ValidateEmail(dto.Email);
            ValidatiionHelper.ValidateUsername(dto.UserName);

            // Generate Ethereum Wallet
            var wallet = _walletService.GenerateWallet();

            // Generate RSA key pair
            var rsaKeys = _keyService.GenerateRsaKeyPair();

            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                WalletAddress = wallet.Address,
                PublicKey = rsaKeys.PublicKeyPem,
                EncryptedPrivateKey = rsaKeys.PrivateKeyPem //  store securely
            };
            _logger.LogInformation("User Creation Beginning");

            var result = await _userManager.CreateAsync(user, dto.Password);
           
            if (!result.Succeeded)
            {
                _logger.LogWarning("User Creation Failed for  {user}:{errors}",dto.Email,string.Join(",",result.Errors.Select(e=>e.Description)));
            }

            var token =  _jwtService.GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Email = user.Email,
                WalletAddress = user.WalletAddress,
                PublicKey = user.PublicKey,
                Token = token
               
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) throw new UnauthorizedAccessException("Invalid login");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded) throw new UnauthorizedAccessException("Invalid login");

            var tokens =  _jwtService.GenerateJwtToken(user);
            var refreshToken = await  _jwtService.CreateRefreshToken(user);

            return new AuthResponseDto
            {
                Email = user.Email,
                WalletAddress = user.WalletAddress,
                PublicKey = user.PublicKey,
                Token = tokens,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<TokenResponse> RefreshTokenAsync(string expiredtoken,string refreshToken)
        {
            return await _jwtService.RefreshAsync(expiredtoken,refreshToken);
        }
    }

}
