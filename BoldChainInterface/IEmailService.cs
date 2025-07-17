using BoldChainBackendAPI.BoldChainModel.BoldChainDto;

namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface IEmailService
    {
        Task SendEmailAsync(SecureEmailMessage message);
    }

}
