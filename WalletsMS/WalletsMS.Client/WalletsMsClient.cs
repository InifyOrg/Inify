using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WalletsMS.Contract;

namespace WalletsMS.Client
{
    public class WalletsMsClient : IWalletsMsClient
    {
        private readonly string? _serviceAddress;
        private readonly string? _apiBaseAddress;
        private bool _configurationReady;
        private readonly HttpClient _httpClient;

        public WalletsMsClient()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("clientsettings.json")
                .Build();

            _serviceAddress = configuration.GetSection("WalletsServiceAddress").Value;
            _apiBaseAddress = configuration.GetSection("WalletsServiceApiBase").Value;
            _configurationReady = !string.IsNullOrEmpty(_apiBaseAddress) && !string.IsNullOrEmpty(_serviceAddress);
            _httpClient = new HttpClient();
        }

        public Task<WalletDTO> AddNewWalletFromDTO(AddWalletDTO addWalletFromDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteWalletById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WalletDTO>> GetAllWalletsByUserId(long userId)
        {
            List<WalletDTO> wallets = new List<WalletDTO>();

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/getAllTokensByWalletType/{userId}";

                HttpResponseMessage walletsMsResponce = await _httpClient.GetAsync(requestAddress);

                string jsonWallet = await walletsMsResponce.Content.ReadAsStringAsync();

                wallets = JsonSerializer.Deserialize<List<WalletDTO>>(jsonWallet);
                return wallets;
            }

            return wallets;
        }
    }
}
