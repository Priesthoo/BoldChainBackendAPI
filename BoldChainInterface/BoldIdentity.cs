using BoldChainBackendAPI.BoldChainConfiguration;
using Microsoft.Extensions.Options;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace BoldChainBackendAPI.BoldChainInterface
{
    public class BoldIdentity : IBoldIdentity
    {
        private readonly IWeb3 _web3;
        private readonly Contract _contract;
        private readonly EthereumSetting _settings;
        private readonly ContractAddress _address;

        public BoldIdentity(IOptions<EthereumSetting> options, IOptions<ContractAddress> Addoptions)
        {
            _settings = options.Value;
            _address = Addoptions.Value;
            var account = new Account(_settings.PrivateKey);
            _web3 = new Web3(account, _settings.RpcUrl);

            var abi = File.ReadAllText("BoldChainSmartContracts/BoldIdentity.json");
            _contract = _web3.Eth.GetContract(abi, _address.BoldIdentity);
        }

        public async Task<string?> GetEmailByWalletAsync(string walletAddress)
        {
            var function = _contract.GetFunction("getEmailForWallet");
            return await function.CallAsync<string>(walletAddress);
        }

        public async Task<string?> GetWalletByEmailAsync(string email)
        {
            var function = _contract.GetFunction("getWalletForEmail");
            return await function.CallAsync<string>(email);
        }

        public async Task<bool> RegisterIdentityAsync(string email, string walletAddress)
        {
            var function = _contract.GetFunction("registerIdentity");

            var gas = await function.EstimateGasAsync(_settings.AccountAddress, null, null, email, walletAddress);
            var receipt = await function.SendTransactionAndWaitForReceiptAsync(_settings.AccountAddress, gas, null, null, email, walletAddress);

            return receipt.Status.Value == 1;
        }
    }

}





