using BoldChainBackendAPI.BoldChainInterface;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace BoldChainBackendAPI.BoldChainDeployment
{
    public class GanacheDeployer : IGanacheDeployer
    {
        private readonly IWeb3 _web3;
        public GanacheDeployer(string rpcurl, string privateKey)
        {
            var account = new Account(privateKey);
            _web3 = new Web3(account, rpcurl);
        }

        private static string LoadAbi(string contract) => File.ReadAllText(Path.Combine("BoldChainContract", contract, $"{contract}.abi"));
        private static string LoadBin(string contract) => File.ReadAllText(Path.Combine("BoldChainContract", contract, $"{contract}.bin"));

        public async Task<string> DeployContractAsync(string contract)
        {
            var abi = LoadAbi(contract);
            var bin = LoadBin(contract);
            var receipt = await _web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(abi, bin,_web3.TransactionManager.Account.Address,new HexBigInteger(5_000_000),null);
            return receipt.ContractAddress;
        }

        public async Task<Dictionary<string,string>> DeployAllContractAsync()
        {
            var results = new Dictionary<string, string>
            {
                ["IdentityRegistry"] = await DeployContractAsync("IdentityRegistry"),
                ["TrustStampRegistry"] = await DeployContractAsync("TrustStampRegistry"),
                ["MessageRegistry"] = await DeployContractAsync("MessageRegistry")
            };

            return results;
        }


    }
}