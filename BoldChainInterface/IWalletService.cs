namespace BoldChainBackendAPI.BoldChainInterface
{
    
    

    public interface IWalletService
    {
        (string Address, string PrivateKey) GenerateWallet();
        bool ValidateSignature(string message, string signature, string address);
    }

}
