using BoldChainBackendAPI.BoldChainInterface;

namespace BoldChainBackendAPI.BoldChainService
{
    public class EmailParser:IEmailParser
    {
        public string GetDkimHeader(string rawEmail)
        {
            var header = rawEmail.Split(new[] {"\r\n","\n"}, StringSplitOptions.None);
            return header.FirstOrDefault(h=>h.StartsWith("DKM-Signature:",StringComparison.OrdinalIgnoreCase));
        }
    }
}
