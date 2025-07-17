using BoldChainBackendAPI.BoldChainConfiguration;
using BoldChainBackendAPI.BoldChainInterface;
using Microsoft.Extensions.Options;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace BoldChainBackendAPI.BoldChainService
{
    public class TrustStampService:ITrustStampService
    {
        private readonly IWeb3 _web3;
        private readonly Contract _contract;
        private readonly ILogger<TrustStampService > _logger;
        public TrustStampService(IOptions<EthereumSetting> ethoptions, IOptions<ContractAddress> contractoptions, ILogger<TrustStampService> logger)
        {
            var abi = File.ReadAllText("BoldChainSmartContracts/TrustStamp.json");
            var contractAddess = contractoptions.Value.TrustStamp;
            _web3 = new Web3(ethoptions.Value.RpcUrl);
            _contract = _web3.Eth.GetContract(abi, contractAddess);
            _logger = logger;
        }

        public async Task<bool> IsTrustedAsync(string address)
        {
            var function = _contract.GetFunction("isTrusted");
            _logger.LogInformation("Getting Function from contract", function.ToString());
            return await function.CallAsync<bool>(address);
        }

        public async Task<string> StampTrustAsync(string userAdddress, string privateKey)
        {
            var account = new Account(privateKey);
            var web3WithSigner = new Web3(account, _web3.Client);
            var function = _contract.GetFunction("stampTrust");
            return await function.SendTransactionAsync(account.Address, new HexBigInteger(300000), null, userAdddress);
        }
    }
}
