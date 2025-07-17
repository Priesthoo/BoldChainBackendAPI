using BoldChainBackendAPI.BoldChainModel.BoldChainDto;
using BoldChainBackendAPI.BoldChainModel.BoldChainEntities;

namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface IUserService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);

        Task<TokenResponse> RefreshTokenAsync(string expiredtoken, string refreshToken);
    }
}
