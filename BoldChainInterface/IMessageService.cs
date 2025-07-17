namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface IMessageService
    {
        string HashCanonicalMessage(string canonicalContent);
        string SignMessage(string hash, string privateKey);
        bool VerifySignature(string hash, string signature, string address);
    }

}
