namespace BoldChainBackendAPI.BoldChainModel.BoldChainDto
{
    public class TrustStatusDto
    {
        public bool IsTrusted { get; set; }
        public DateTime? VerifiedOn { get; set; }
        public string VerificationSource { get; set; } = string.Empty; // "SmartContract", "Admin", "Email", etc.
        public List<string> EndorsedBy { get; set; } = new(); // Optional: List of endorsing addresses
    }

}
