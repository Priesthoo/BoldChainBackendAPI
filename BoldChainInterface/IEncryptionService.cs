using System.Security.Cryptography;

namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface IEncryptionService
    {
        (string EncryptedData, string EncryptedKey) Encrypt(string plainText, RSAParameters rsaPublicKey);
        string Decrypt(string encryptedData, string encryptedKey, RSAParameters rsaPrivateKey);
    }
}
