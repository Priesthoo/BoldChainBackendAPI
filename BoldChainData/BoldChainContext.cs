using BoldChainBackendAPI.BoldChainModel.BoldChainEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoldChainBackendAPI.BoldChainData
{
    public class BoldChainContext:IdentityDbContext<User>
    {
        public BoldChainContext(DbContextOptions<BoldChainContext> options) : base(options) { }
        
        
        public DbSet<EnterpriseIdentity> enterpriseIdentities { get; set; }
        public DbSet<AuditLog> auditLogs { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        public DbSet<MessageMeta> messagesMeta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           modelBuilder.Entity<User>().HasIndex(u=>u.Email).IsUnique();
            modelBuilder.Entity<MessageMeta>().HasOne(t => t.Sender).WithMany(t => t.Inbox).HasForeignKey(m => m.SenderId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MessageMeta>().HasOne(t=>t.Recipient).WithMany(t => t.Outbox).HasForeignKey(m => m.RecipientId);
            

            modelBuilder.Entity<RefreshToken>().HasOne(t => t.User).WithMany().HasForeignKey(t => t.UserId);
        }
    }
}
