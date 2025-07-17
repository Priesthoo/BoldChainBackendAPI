using BoldChainBackendAPI.BoldChainInterface;

namespace BoldChainBackendAPI.BoldChainService
{
    public class CanonicalizationService : ICanonicalizationService
    {
        public string Canonicalize(string emailContent)
        {
            return emailContent
                .Replace("\r\n", "\n")
                .Replace("\r", "\n")
                .Trim()
                .ToLowerInvariant();
        }
    }

}
