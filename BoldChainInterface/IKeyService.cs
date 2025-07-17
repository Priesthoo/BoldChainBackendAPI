using System.Security.Cryptography;

namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface IKeyService
    {
        (string PublicKeyPem, string PrivateKeyPem) GenerateRsaKeyPair();
        RSAParameters GetPublicKeyParams(string publicKeyPem);
        RSAParameters GetPrivateKeyParams(string privateKeyPem);
    }

}
