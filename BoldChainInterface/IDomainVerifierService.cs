namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface IDomainVerifierService
    {
        Task<bool> isDomainVerifiedAsync(string domain);
        Task<string> VerifyDomainAsync(string domain,string privateKey);
    }
}
