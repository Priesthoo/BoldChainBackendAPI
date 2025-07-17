using BoldChainBackendAPI.BoldChainInterface;
using System.Security.Cryptography;
using System.Text;

namespace BoldChainBackendAPI.BoldChainService
{
    public class EncryptionService : IEncryptionService
    {
        public (string EncryptedData, string EncryptedKey) Encrypt(string plainText, RSAParameters rsaPublicKey)
        {
            using var aes = Aes.Create();
            aes.GenerateKey();
            aes.GenerateIV();

            byte[] encryptedData;
            using (var encryptor = aes.CreateEncryptor())
            {
                var bytes = Encoding.UTF8.GetBytes(plainText);
                encryptedData = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            }

            byte[] encryptedAesKey;
            using (var rsa = RSA.Create())
            {
                rsa.ImportParameters(rsaPublicKey);
                encryptedAesKey = rsa.Encrypt(aes.Key, RSAEncryptionPadding.OaepSHA256);
            }

            return (
                Convert.ToBase64String(encryptedData),
                Convert.ToBase64String(encryptedAesKey)
            );
        }

        public string Decrypt(string encryptedData, string encryptedKey, RSAParameters rsaPrivateKey)
        {
            byte[] decryptedAesKey;
            using (var rsa = RSA.Create())
            {
                rsa.ImportParameters(rsaPrivateKey);
                decryptedAesKey = rsa.Decrypt(Convert.FromBase64String(encryptedKey), RSAEncryptionPadding.OaepSHA256);
            }

            using var aes = Aes.Create();
            aes.Key = decryptedAesKey;
            aes.IV = new byte[16]; // Assuming IV was fixed or communicated separately

            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
            var decryptedBytes = aes.CreateDecryptor().TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }

}
