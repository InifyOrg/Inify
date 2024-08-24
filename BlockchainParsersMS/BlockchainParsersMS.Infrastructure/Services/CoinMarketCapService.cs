using BlockchainParsersMS.Contract;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainParsersMS.Infrastructure.Services
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

        public async Task<List<ParsedTokenDTO>> ParsePricesOfParsedTokens(List<ParsedTokenDTO> parsedTokensWithoutPrice)
        {
            string slugs = "";

            foreach (var token in parsedTokensWithoutPrice)
                slugs += $"{token.Slug},";

            if (_configurationReady && !string.IsNullOrEmpty(slugs))
            {
                slugs = slugs.Remove(slugs.Length - 1);
                string requestAddress = $"{_apiBaseAddress}/v2/cryptocurrency/quotes/latest?slug={slugs}&convert=USD";

                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestAddress);

                httpRequest.Headers.Add("X-CMC_PRO_API_KEY", _apiKey);

                HttpResponseMessage httpResponce = await _httpClient.SendAsync(httpRequest);

                string json = await httpResponce.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(json);
                var res = (decimal)jo["data"].Values().First()["quote"]["USD"]["price"];

                for (int i = 0; i < parsedTokensWithoutPrice.Count; i++)
                {
                    parsedTokensWithoutPrice[i].Price = (decimal)jo["data"].Values().First(d => (string)d["slug"] == parsedTokensWithoutPrice[i].Slug)["quote"]["USD"]["price"];
                    parsedTokensWithoutPrice[i].UsdValue = parsedTokensWithoutPrice[i].Price * parsedTokensWithoutPrice[i].Amount;
                }
            }

            return parsedTokensWithoutPrice;
        }
    }
}
