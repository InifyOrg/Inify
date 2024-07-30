using Mapster;
using Microsoft.Extensions.Configuration;
using Nethereum.ABI.CompilationMetadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TokensMS.Contract;
using TokensMS.Infrastructure.Domain.Entities;
using TokensMS.Infrastructure.Web3DTOs;

namespace TokensMS.Infrastructure.Services
{
    public class CoinMarketCapService: ICoinMarketCapService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiBaseAddress;
        private readonly string? _apiKey;
        private bool _configurationReady;
        private readonly ITokensDataLayer _tokensDataLayer;
        private readonly IWeb3Service _web3Service;

        public CoinMarketCapService(ITokensDataLayer tokensDataLayer, IWeb3Service web3Service)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _apiBaseAddress = configuration.GetSection("APIs:CoinMarketCapAPIBase").Value;
            _apiKey = configuration.GetSection("APIs:CoinMarketCapAPIKey").Value;
            _configurationReady = !string.IsNullOrEmpty(_apiBaseAddress) && !string.IsNullOrEmpty(_apiKey);
            _httpClient = new HttpClient();

            _tokensDataLayer = tokensDataLayer;
            _web3Service = web3Service;
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
            //ToDo: сделать чтобы вытаскивались данные не только о ethereum а о всех слагах которые добавлены в бд платформ
            List<CoinMarketCapTokenDTO> coinMarketCapFilteredTokens = new List<CoinMarketCapTokenDTO>(responceDTO.data.Where(d => d.platform != null && d.platform.slug == "ethereum").ToList().OrderBy(token => token.rank));
            List<TokenDTO> adaptedTokens = coinMarketCapFilteredTokens.Adapt<List<TokenDTO>>().GetRange(0, 50);
            List<TokenDTO> tokensToAdd = new List<TokenDTO>();

            for (int i = 0; i < adaptedTokens.Count; i++)
            {
                adaptedTokens[i].Address = coinMarketCapFilteredTokens[i].platform.token_address;
                //ToDo: сделать связь между платформами и типами кошельков (ethereum == EVM | bnb == EVM, etc..)
                //пока что хардкод потому что добавляю только эфир и евм
                adaptedTokens[i].WalletType = new WalletTypeDTO() { Title= "EVM" };
            }

            IEnumerable<Task> tasks = adaptedTokens.Select(async token => {
                token.Decimals = await _web3Service.GetDecimalsByAddress(token.Address);
                tokensToAdd.Add(token);
            });
            await Task.WhenAll(tasks);

            List<Token> addedTokens = await _tokensDataLayer.AddRangeTokens(tokensToAdd.Adapt<List<Token>>());

            return addedTokens.Adapt<List<TokenDTO>>();
        }
    }
}
