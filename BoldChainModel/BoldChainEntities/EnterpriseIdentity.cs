namespace BoldChainBackendAPI.BoldChainModel.BoldChainEntities
{
    public class EnterpriseIdentity
    {
        public Guid Id { get; set; }    
        public string Domain { get; set; }
        public string ContactEmail { get; set; }
        public string WalletAddress { get; set; }
        public string Status { get; set; }
    }
}
