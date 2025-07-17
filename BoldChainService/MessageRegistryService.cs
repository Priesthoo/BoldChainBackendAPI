using BoldChainBackendAPI.BoldChainConfiguration;
using BoldChainBackendAPI.BoldChainInterface;
using Microsoft.Extensions.Options;
using Nethereum.Contracts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace BoldChainBackendAPI.BoldChainService
{
    public class MessageRegistryService:IMessageRegistryService
    {
        private readonly IWeb3 _web3;
        private readonly Contract _contract;
        public MessageRegistryService(IOptions<EthereumSetting> ethoptions, IOptions<ContractAddress> contractoptions)
        {
            var abi = File.ReadAllText("BoldChainSmartContracts/MessageRegistry.json");
            var contractAddress = contractoptions.Value.MessageRegistry;
            _web3 = new Web3(ethoptions.Value.RpcUrl);
            _contract = _web3.Eth.GetContract(abi, contractAddress);
        }

        public async Task<string> RecordMessageAsync(string senderHash, string recipientHash, string canonicalHash, string subjectHash, long timestamp, string ipfsCid, string privateKey)
        {
            var account = new Account(privateKey);
            var webWithSigner = new Web3(account,_web3.Client);
            var function = _contract.GetFunction("recordMessage");
            return await function.SendTransactionAsync(account.Address, new HexBigInteger(400000), null, senderHash.HexToByteArray(), recipientHash.HexToByteArray(), canonicalHash.HexToByteArray(), subjectHash.HexToByteArray(), timestamp, ipfsCid);
        }
    }
}
