using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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
                .AddJsonFile("walletsclientsettings.json")
                .Build();

            _serviceAddress = configuration.GetSection("WalletsServiceAddress").Value;
            _apiBaseAddress = configuration.GetSection("WalletsServiceApiBase").Value;
            _configurationReady = !string.IsNullOrEmpty(_apiBaseAddress) && !string.IsNullOrEmpty(_serviceAddress);
            _httpClient = new HttpClient();
        }

        public async Task<WalletDTO> AddNewWalletFromDTO(AddWalletDTO addWalletFromDTO)
        {
            WalletDTO wallet = new WalletDTO();

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/addNewWallet";
                HttpContent postContetnt = JsonContent.Create(addWalletFromDTO);

                HttpResponseMessage walletsMsResponce = await _httpClient.PostAsync(requestAddress, postContetnt);

                string jsonWallet = await walletsMsResponce.Content.ReadAsStringAsync();

                wallet = JsonSerializer.Deserialize<WalletDTO>(jsonWallet);
            }

            return wallet;
        }

        public async Task<bool> DeleteWalletById(long id)
        {
            bool isDeleted = false;

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/{id}";

                HttpResponseMessage walletsMsResponce = await _httpClient.DeleteAsync(requestAddress);

                string jsonWallet = await walletsMsResponce.Content.ReadAsStringAsync();

                isDeleted = bool.Parse(jsonWallet);
            }

            return isDeleted;
        }

        public async Task<List<WalletDTO>> GetAllWalletsByUserId(long userId)
        {
            List<WalletDTO> wallets = new List<WalletDTO>();

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/getAllWalletsByUserId/{userId}";

                HttpResponseMessage walletsMsResponce = await _httpClient.GetAsync(requestAddress);

                string jsonWallet = await walletsMsResponce.Content.ReadAsStringAsync();

                if (walletsMsResponce.IsSuccessStatusCode)
                {
                    wallets = JsonSerializer.Deserialize<List<WalletDTO>>(jsonWallet);
                }
            }

            return wallets;
        }

        public async Task<List<WalletTypeDTO>> GetAllWalletTypes()
        {
            List<WalletTypeDTO> wallets = new List<WalletTypeDTO>();

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/getAllWalletTypes";

                HttpResponseMessage walletsMsResponce = await _httpClient.GetAsync(requestAddress);

                string jsonWallet = await walletsMsResponce.Content.ReadAsStringAsync();

                wallets = JsonSerializer.Deserialize<List<WalletTypeDTO>>(jsonWallet);
            }

            return wallets;
        }

    }
}
