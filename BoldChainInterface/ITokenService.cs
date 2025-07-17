using BoldChainBackendAPI.BoldChainModel.BoldChainDto;
using BoldChainBackendAPI.BoldChainModel.BoldChainEntities;

namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
        Task<RefreshToken> CreateRefreshToken(User user);
        Task<TokenResponse> RefreshAsync(string expiredAccessToken, string refreshToken);   

    }
}
