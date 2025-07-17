namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface IBoldIdentity
    {
        Task<string?> GetEmailByWalletAsync(string walletAddress);
        Task<string?> GetWalletByEmailAsync(string email);
        Task<bool> RegisterIdentityAsync(string email, string walletAddress);
    }

}
