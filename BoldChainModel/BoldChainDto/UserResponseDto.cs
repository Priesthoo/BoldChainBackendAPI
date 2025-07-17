namespace BoldChainBackendAPI.BoldChainModel.BoldChainDto
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string WalletAddress { get; set; } = string.Empty;
        public string PublicKey { get; set; } = string.Empty;
        public string TrustStamp { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public bool IsWalletVerified { get; set; }
        public string IdentityContractAddress { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public bool DomainVerified { get; set; }
        public string DisplayName { get; set; } = string.Empty;

        // Trust & Reputation info
        public TrustStatusDto TrustStatus { get; set; } = new();
    }

}
