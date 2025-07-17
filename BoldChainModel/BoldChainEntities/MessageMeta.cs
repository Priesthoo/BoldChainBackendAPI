using Org.BouncyCastle.Crypto.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoldChainBackendAPI.BoldChainModel.BoldChainEntities
{
    public class MessageMeta
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public string CanonicalHash { get; set; }
        public string Signature { get; set; }
        public string SenderWalletAddress { get; set; }
        public string? EncryptedKey { get; set; }
        public string TrustStamp { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
      
        [ForeignKey("Sender")]
        public Guid SenderId { get; set; }
       
        public User Sender { get; set; }
      
        public User Recipient {  get; set; }
    
        [ForeignKey("Recipient")]
        public Guid RecipientId { get; set; }
    }
}
