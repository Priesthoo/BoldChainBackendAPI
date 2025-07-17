using BoldChainBackendAPI.BoldChainData;
using BoldChainBackendAPI.BoldChainInterface;
using BoldChainBackendAPI.BoldChainModel.BoldChainEntities;
using Microsoft.EntityFrameworkCore;

namespace BoldChainBackendAPI.BoldChainService
{
    public class AuditService : IAuditService
    {
        private readonly BoldChainContext _context;

        public AuditService(BoldChainContext context)
        {
            _context = context;
        }

        public async Task LogAsync(AuditLog log)
        {
            log.Timestamp = DateTime.UtcNow;
            _context.auditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetLogsByEmailAsync(string email)
        {
            return await _context.auditLogs
                .Where(x => x.SenderEmail == email || x.RecipientEmail == email)
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();
        }
    }

}
