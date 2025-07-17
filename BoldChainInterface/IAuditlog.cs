using BoldChainBackendAPI.BoldChainModel.BoldChainEntities;

namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface IAuditService
    {
        Task LogAsync(AuditLog log);
        Task<IEnumerable<AuditLog>> GetLogsByEmailAsync(string email);
    }

}
