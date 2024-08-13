using BlockchainParsersMS.Infrastructure.Web3DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlockchainParsersMS.Infrastructure.Services
{
    public class CoinGeckoService : ICoinGeckoService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiBaseAddress;
        private readonly string? _apiKey;
        private bool _configurationReady;

        public CoinGeckoService()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _apiBaseAddress = configuration.GetSection("APIs:CoinGeckoAPIBase").Value;
            _apiKey = configuration.GetSection("APIs:CoinGeckoAPIKey").Value;
            _configurationReady = !string.IsNullOrEmpty(_apiBaseAddress) && !string.IsNullOrEmpty(_apiKey);
            _httpClient = new HttpClient();

        }

        public async Task<decimal> GetPriceByCoinId(string id)
        {
            decimal res = 1;

            if (_configurationReady)
            {
                string requestAddress = $"{_apiBaseAddress}/v3/coins/{id}";

                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestAddress);

                httpRequest.Headers.Add("x_cg_demo_api_key", _apiKey);

                HttpResponseMessage httpResponce = await _httpClient.SendAsync(httpRequest);

                string json = await httpResponce.Content.ReadAsStringAsync();

                CoinGeckoCoinByIdApiResponce coinGeckoDeserializedResponce = JsonSerializer.Deserialize<CoinGeckoCoinByIdApiResponce>(json);

                res = coinGeckoDeserializedResponce.market_data.current_price.usd;
            }

            return res;
        }
    }
}
