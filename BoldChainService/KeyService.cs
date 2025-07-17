using BoldChainBackendAPI.BoldChainInterface;
using System.Security.Cryptography;

namespace BoldChainBackendAPI.BoldChainService
{
    public class KeyService : IKeyService
    {
        public (string PublicKeyPem, string PrivateKeyPem) GenerateRsaKeyPair()
        {
            using var rsa = RSA.Create(2048);
            var privateKey = rsa.ExportRSAPrivateKey();
            var publicKey = rsa.ExportRSAPublicKey();

            return (
                Convert.ToBase64String(publicKey),
                Convert.ToBase64String(privateKey)
            );
        }

        public RSAParameters GetPublicKeyParams(string publicKeyPem)
        {
            using var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKeyPem), out _);
            return rsa.ExportParameters(false);
        }

        public RSAParameters GetPrivateKeyParams(string privateKeyPem)
        {
            using var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKeyPem), out _);
            return rsa.ExportParameters(true);
        }
    }


}
