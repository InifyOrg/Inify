using BlockchainParsersMS.Contract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlockchainParsersMS.Client
{
    public class BlockchainParserClient: IBlockchainParserClient
    {
        private readonly string? _serviceAddress;
        private readonly string? _apiBaseAddress;
        private bool _configurationReady;
        private readonly HttpClient _httpClient;

        public BlockchainParserClient()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("parsersclientsettings.json")
                .Build();

            _serviceAddress = configuration.GetSection("ParsersServiceAddress").Value;
            _apiBaseAddress = configuration.GetSection("ParsersServiceApiBase").Value;
            _configurationReady = !string.IsNullOrEmpty(_apiBaseAddress) && !string.IsNullOrEmpty(_serviceAddress);
            _httpClient = new HttpClient();
        }

        public async Task<ParsingOutputDTO> parseOneByAddress(WalletDTO walletInfo)
        {
            ParsingOutputDTO output = new ParsingOutputDTO();

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/parseOneByAddress?Address={walletInfo.Address}&Type={walletInfo.Type}";

                HttpResponseMessage tokensMsResponce = await _httpClient.GetAsync(requestAddress);

                string jsonToken = await tokensMsResponce.Content.ReadAsStringAsync();

                output = JsonSerializer.Deserialize<ParsingOutputDTO>(jsonToken);
                return output;
            }

            return output;
        }

        public async Task<ParsingOutputDTO> parseManyByUserId(long userId)
        {
            ParsingOutputDTO output = new ParsingOutputDTO();

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/parseManyByUserId/{userId}";

                HttpResponseMessage tokensMsResponce = await _httpClient.GetAsync(requestAddress);

                string jsonToken = await tokensMsResponce.Content.ReadAsStringAsync();

                output = JsonSerializer.Deserialize<ParsingOutputDTO>(jsonToken);
                return output;
            }

            return output;
        }

    }
}
