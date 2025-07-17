namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface IGanacheDeployer
    {
        Task<string> DeployContractAsync (string contractName);
        Task<Dictionary<string, string>> DeployAllContractAsync();
    }
}
