using BoldChainBackendAPI.BoldChainConfiguration;
using BoldChainBackendAPI.BoldChainInterface;
using Microsoft.Extensions.Options;
using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Diagnostics.Contracts;
using Contract = Nethereum.Contracts.Contract;

namespace BoldChainBackendAPI.BoldChainService
{
    public class IdentityRegistryService:IIdentityRegistryService
    {
        private readonly IWeb3 web3;
        private readonly Contract _contract;
        
        
        public IdentityRegistryService(IOptions<EthereumSetting> ethoptions,IOptions<ContractAddress> contractoptions)
        {
            
            var abi = File.ReadAllText("BoldChainSmartContracts/IdentityRegistryjson.json");
            var contractAddress = contractoptions.Value.IdentityRegistry;
            web3 = new Web3( ethoptions.Value.RpcUrl);
            _contract = web3.Eth.GetContract(abi, contractAddress);

         }
        public async Task<bool> IsVerifiedAsync(string userAddress)
        {
            var func = _contract.GetFunction("isVerified");
            return await func.CallAsync<bool>(userAddress);

        }
        public async Task<string> VerifyAsync(string userAddress, string privateKey)
        {
            var account = new Account(privateKey);
            var webwithSigner = new Web3(account, web3.Client);
            var function = _contract.GetFunction("verify");
            return await function.SendTransactionAsync(account.Address, new HexBigInteger(30000), userAddress);
        }
    }
}
