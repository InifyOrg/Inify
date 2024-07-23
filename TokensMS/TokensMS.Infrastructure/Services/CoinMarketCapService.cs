using Mapster;
using Microsoft.Extensions.Configuration;
using Nethereum.ABI.CompilationMetadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TokensMS.Contract;
using TokensMS.Infrastructure.Web3DTOs;

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

        private async Task<string> GetTokensJson()
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

        public async Task<List<TokenDTO>> UpdateDatabase()
        {
            string json = await GetTokensJson();

            CoinMarketCapApiMapResponceDTO responceDTO = JsonSerializer.Deserialize<CoinMarketCapApiMapResponceDTO>(json);
            List<CoinMarketCapTokenDTO> coinMarketCapFilteredTokens = new List<CoinMarketCapTokenDTO>(responceDTO.data.Where(d => d.platform != null && d.platform.slug == "ethereum").ToList());
            List<AddTokenDTO> tokensToAdd = coinMarketCapFilteredTokens.Adapt<List<AddTokenDTO>>();
            
            for(int i = 0; i<tokensToAdd.Count; i++)
            {
                tokensToAdd[i].Address = coinMarketCapFilteredTokens[i].platform.token_address;
                tokensToAdd[i].WalletType = new AddWalletTypeDTO() { Title= "EVM" };
            }

            return tokensToAdd.Adapt<List<TokenDTO>>();
        }
    }
}
