using Microsoft.AspNetCore.Identity;

namespace BoldChainBackendAPI.BoldChainModel.BoldChainEntities
{
    public class User:IdentityUser<Guid>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PublicKey { get; set; }
        public string WalletAddress { get; set; }
        public string EncryptedPrivateKey { get; set; }
        public bool IsVerifiedOnChain { get; set; } = false;
        public ICollection<MessageMeta> Inbox { get; set; } = new List<MessageMeta>();
        public ICollection<MessageMeta> Outbox { get; set; } = new List<MessageMeta>();
        public ICollection<MessageMeta> Drafts { get; set; } = new List<MessageMeta>();
        public DateTime CreatedAt { get; set; }
    }
}
