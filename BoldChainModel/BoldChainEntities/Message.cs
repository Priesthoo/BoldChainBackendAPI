using System.ComponentModel.DataAnnotations.Schema;

namespace BoldChainBackendAPI.BoldChainModel.BoldChainEntities
{
    public class Message
    {
       public Guid Id { get; set; }
        public string SubjectHash { get; set; } = null!;
        public string BodyCID { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string BlockChainTxId { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public bool IsDraft { get; set; } = false;
        // Foreign Keys
     
        public Guid? SenderId { get; set; }
      
        public Guid? RecipientId { get; set; }
        //Navigation Properties
      
        public User? SenderUser { get; set; }
      
        public User? RecipientUser { get; set; }

    


    }
}
