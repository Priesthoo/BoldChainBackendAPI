using BoldChainBackendAPI.BoldChainConfiguration;
using BoldChainBackendAPI.BoldChainInterface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace BoldChainBackendAPI.BoldChainService
{
    public class PinataService:IPinataService
    {
        private readonly PinataSettings _settings;
        private readonly HttpClient _httpClient;
        public PinataService(IOptions<PinataSettings> settings)
        {
            _settings = settings.Value;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_settings.BaseUrl)
            };
            _httpClient.DefaultRequestHeaders.Add("pinata_api_key", _settings.ApiKey);
            _httpClient.DefaultRequestHeaders.Add("pinata_secret_api_key",_settings.ApiSecret);
        }
        public async Task<string> PinTextAsync(string name, string content)
        {
            var body = new
            {
                pinataMetadata = new { name = name },
                pinatContent = content
            };
            var json = JsonConvert.SerializeObject(body);
            var response = await _httpClient.PostAsync("/pinning/pinJSONToIPFS", new StringContent(json,Encoding.UTF8,"application/json"));
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseBody);
            return result.IpfsHash;
        }
        public async Task<string> GetPinnedContentAsync(string ipfsHash)
        {
            var gatewayUrl = $"https://gateway.pinata.cloud/ipfs/{ipfsHash}";
            var result = await _httpClient.GetAsync(gatewayUrl);
            result.EnsureSuccessStatusCode() ;
            return await result.Content.ReadAsStringAsync();
        }
        public async Task<bool> UnpinAsync(string ipfsHash)
        {
            var response = await _httpClient.DeleteAsync($"/pinning/unpin/{ipfsHash}");
            return response.IsSuccessStatusCode;
        }

    }
}
