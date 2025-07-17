namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface ITrustStampService
    {
        Task<bool> IsTrustedAsync(string address);
        Task<string> StampTrustAsync(string userAddress, string privateKey);
    }
}
