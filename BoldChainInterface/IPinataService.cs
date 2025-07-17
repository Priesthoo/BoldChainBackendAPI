namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface IPinataService
    {
        Task<string> PinTextAsync(string name, string content);
        Task<string> GetPinnedContentAsync(string ipfsHash);
        Task<bool> UnpinAsync(string ipfsHash);
    }
}
