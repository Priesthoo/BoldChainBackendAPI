namespace BoldChainBackendAPI.BoldChainConfiguration
{
    public class ContractAddress
    {
        public string DomainVerifier { get; set; } = null!;
        public string IdentityRegistry { get; set; } = null!;
        public string MessageRegistry { get; set; } = null!;
        public string TrustStamp { get; set; } = null!;
        public string BoldIdentity { get; set; } = null!;
    }
}
