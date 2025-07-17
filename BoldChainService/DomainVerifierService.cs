using BoldChainBackendAPI.BoldChainConfiguration;
using BoldChainBackendAPI.BoldChainInterface;
using Microsoft.Extensions.Options;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Model;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Account = Nethereum.Web3.Accounts.Account;

namespace BoldChainBackendAPI.BoldChainService
{
    public class DomainVerifierService : IDomainVerifierService
    {
        private readonly IWeb3 _web3;
        private readonly Contract _contract;
        public DomainVerifierService(IOptions<EthereumSetting> ethoptions, IOptions<ContractAddress> contractoptions)
        {
            var abi = File.ReadAllText("BoldChainSmartContracts/DomainVerifier.json");
            var contractAddress = contractoptions.Value.DomainVerifier;
            _web3 = new Web3(ethoptions.Value.RpcUrl);
            _contract = _web3.Eth.GetContract(abi, contractAddress);

        }
        public async Task<bool> isDomainVerifiedAsync(string domain)
        {
            var function = _contract.GetFunction("isDomainVerified");
            return await function.CallAsync<bool>(domain);
        }
        public async Task<string> VerifyDomainAsync(string domain, string privateKey)
        {
            var account = new Account(privateKey);
            var web3withSigner = new Web3(account, _web3.Client);
            var function = _contract.GetFunction("verifyDomain");
            return await function.SendTransactionAsync(account.Address, new HexBigInteger(30000), null, domain);
        }
    }       
} 
