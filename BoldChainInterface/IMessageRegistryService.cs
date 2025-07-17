using System.Security;

namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface IMessageRegistryService
    {
        Task<string> RecordMessageAsync(string senderHash, string recipientHash, string canonicalHash, string subjectHash, long timestamp, string ipfsCid, string privateKey);

    }
}
