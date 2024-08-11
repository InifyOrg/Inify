using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Task<decimal> GetPriceByCoinId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
