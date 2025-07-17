using BoldChainBackendAPI.BoldChainInterface;
using Nethereum.Signer;

namespace BoldChainBackendAPI.BoldChainService
{
    public class WalletService : IWalletService
    {
        public (string Address, string PrivateKey) GenerateWallet()
        {
            var ecKey = EthECKey.GenerateKey();
            var privateKey = ecKey.GetPrivateKey();
            var address = ecKey.GetPublicAddress();

            return (address, privateKey);
        }

        public bool ValidateSignature(string message, string signature, string address)
        {
            var signer = new EthereumMessageSigner();
            var recovered = signer.EncodeUTF8AndEcRecover(message, signature);
            return string.Equals(recovered, address, StringComparison.OrdinalIgnoreCase);
        }
    }

}
