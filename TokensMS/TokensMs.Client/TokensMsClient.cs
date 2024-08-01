using Microsoft.Extensions.Configuration;
using System.Text.Json;
using TokensMS.Contract;

namespace TokensMs.Client
{
    public class TokensMsClient : ITokensMsClient
    {
        private readonly string? _serviceAddress;
        private readonly string? _apiBaseAddress;
        private bool _configurationReady;
        private readonly HttpClient _httpClient;

        public TokensMsClient()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("clientsettings.json")
                .Build();

            _serviceAddress = configuration.GetSection("TokensServiceAddress").Value;
            _apiBaseAddress = configuration.GetSection("TokensServiceApiBase").Value;
            _configurationReady = !string.IsNullOrEmpty(_apiBaseAddress) && !string.IsNullOrEmpty(_serviceAddress);
            _httpClient = new HttpClient();
        }

        public async Task<List<TokenDTO>> GetAllTokens()
        {
            List<TokenDTO> tokens = new List<TokenDTO>();

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/getAllTokens";

                HttpResponseMessage tokensMsResponce = await _httpClient.GetAsync(requestAddress);

                string jsonToken = await tokensMsResponce.Content.ReadAsStringAsync();

                tokens = JsonSerializer.Deserialize<List<TokenDTO>>(jsonToken);
                return tokens;
            }

            return tokens;
        }

        public async Task<List<TokenDTO>> GetAllTokensByWalletType(string walletType)
        {
            List<TokenDTO> tokens = new List<TokenDTO>();

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/getAllTokensByWalletType/{walletType}";

                HttpResponseMessage tokensMsResponce = await _httpClient.GetAsync(requestAddress);

                string jsonToken = await tokensMsResponce.Content.ReadAsStringAsync();

                tokens = JsonSerializer.Deserialize<List<TokenDTO>>(jsonToken);
                return tokens;
            }

            return tokens;
        }
    }
}