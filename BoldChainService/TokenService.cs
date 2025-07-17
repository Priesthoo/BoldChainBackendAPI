using BoldChainBackendAPI.BoldChainConfiguration;
using BoldChainBackendAPI.BoldChainData;
using BoldChainBackendAPI.BoldChainInterface;
using BoldChainBackendAPI.BoldChainModel.BoldChainEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

using BoldChainBackendAPI.BoldChainModel.BoldChainDto;
using Microsoft.EntityFrameworkCore;

namespace BoldChainBackendAPI.BoldChainService
{
    public class TokenService:ITokenService
    {
        private readonly JwtSettings _settings;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BoldChainContext _context;
        public TokenService(IOptions<JwtSettings> settings, UserManager<User> userManager, IUnitOfWork unitOfWork,BoldChainContext context)
        {
            _settings = settings.Value;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _context = context;
            
        }
        public string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var cred = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("publickey",user.PublicKey),
                new Claim("wallet", user.WalletAddress),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(_settings.Issuer,_settings.Audience, claims, expires: DateTime.UtcNow.AddMinutes(30),signingCredentials:cred);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<RefreshToken> CreateRefreshToken(User user)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString("N"),
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
            };
            await  _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();
            return refreshToken;

        }

        public async Task<TokenResponse> RefreshAsync(string expiredToken, string refreshToken)
        {
            var hamdler = new JwtSecurityTokenHandler();
            var jwt = hamdler.ReadJwtToken(expiredToken);
            var email = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if (email == null)
                throw new SecurityTokenException("Invalid token");
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("User not found");
            var tokenEntry = await _context.refreshTokens.FirstOrDefaultAsync(e => e.Token == refreshToken && e.UserId == user.Id && !e.IsRevoked);
            if (tokenEntry == null || tokenEntry.ExpiresAt < DateTime.UtcNow)
                throw new SecurityTokenException("Invalid or Expired refresh token");
            tokenEntry.IsRevoked = true;
            var newJwt = GenerateJwtToken(user);
            var newRefresh = await CreateRefreshToken(user);
            _unitOfWork.SaveChangesAsync();
            return new TokenResponse
            {
                AccessToken = newJwt,
                RefreshToken = newRefresh.Token,
            };
        }
    }
}
