namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface IIdentityRegistryService
    {
        Task<bool> IsVerifiedAsync(string userAddress);
        Task<string> VerifyAsync(string userAddress,string privateKey);
    }
}
