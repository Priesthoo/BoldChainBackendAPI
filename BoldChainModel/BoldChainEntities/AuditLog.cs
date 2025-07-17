namespace BoldChainBackendAPI.BoldChainModel.BoldChainEntities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string MessageHash { get; set; }
        public string SenderEmail { get; set; }
        public string SenderWallet { get; set; }
        public string RecipientEmail { get; set; }
        public string TrustStatus { get; set; } // VERIFIED, TAMPERED, etc.
        public DateTime Timestamp { get; set; }
        public string Action { get; set; } // SIGNED, VERIFIED, etc.
        public string AdditionalInfo { get; set; } // Optional debug context
    }

}
