using AutoMapper;
using BoldChainBackendAPI.BoldChainInterface;
using BoldChainBackendAPI.BoldChainModel.BoldChainDto;
using BoldChainBackendAPI.BoldChainModel.BoldChainEntities;
using Microsoft.AspNetCore.Identity;

namespace BoldChainBackendAPI.BoldChainService
{
    public class UserService : IUserService
    {
        private readonly UserManager< User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _jwtService;
        private readonly IWalletService _walletService;
        private readonly IKeyService _keyService;
        
        private readonly IBoldIdentity _identity;

        public UserService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService jwtService,
            IWalletService walletService,
            IKeyService keyService,
            IBoldIdentity identity)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _walletService = walletService;
            _keyService = keyService;
            _identity = identity;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null) throw new Exception("Email already in use");

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

            var result = await _userManager.CreateAsync(user, dto.Password);
            await _identity.RegisterIdentityAsync(dto.Email, wallet.Address);
            if (!result.Succeeded) throw new Exception("User creation failed");

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
