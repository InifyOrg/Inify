using Microsoft.Extensions.Configuration;
using Nethereum.ABI.CompilationMetadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokensMS.Infrastructure.Services
{
    public class CoinMarketCapService: ICoinMarketCapService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiBaseAddress;
        private readonly string? _apiKey;
        private bool _configurationReady;

        public CoinMarketCapService()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _apiBaseAddress = configuration.GetSection("APIs:CoinMarketCapAPIBase").Value;
            _apiKey = configuration.GetSection("APIs:CoinMarketCapAPIKey").Value;
            _configurationReady = !string.IsNullOrEmpty(_apiBaseAddress) && !string.IsNullOrEmpty(_apiKey);
            _httpClient = new HttpClient();
        }

        public async Task<string> GetTokensJson()
        {
            string json = "";

            if (_configurationReady)
            {
                string requestAddress = $"{_apiBaseAddress}/v1/cryptocurrency/map";

                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestAddress);

                httpRequest.Headers.Add("X-CMC_PRO_API_KEY", _apiKey);

                HttpResponseMessage httpResponce = await _httpClient.SendAsync(httpRequest);

                json = await httpResponce.Content.ReadAsStringAsync();

            }

            return json;
        }
    }
}
