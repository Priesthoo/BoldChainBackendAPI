using BoldChainBackendAPI.BoldChainInterface;
using Nethereum.Signer;
using System.Security.Cryptography;
using System.Text;

namespace BoldChainBackendAPI.BoldChainService
{
    public class MessageService : IMessageService
    {
        public string HashCanonicalMessage(string canonicalContent)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(canonicalContent));
            return Convert.ToHexString(hash).ToLowerInvariant();
        }

        public string SignMessage(string hash, string privateKey)
        {
            var signer = new EthereumMessageSigner();
            return signer.EncodeUTF8AndSign(hash, new EthECKey(privateKey));
        }

        public bool VerifySignature(string hash, string signature, string address)
        {
            var signer = new EthereumMessageSigner();
            var recovered = signer.EncodeUTF8AndEcRecover(hash, signature);
            return string.Equals(recovered, address, StringComparison.OrdinalIgnoreCase);
        }
    }

}
