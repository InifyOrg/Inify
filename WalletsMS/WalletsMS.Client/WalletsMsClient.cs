using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Task<WalletTypeDTO> AddNewWalletTypeFromDTO(AddWalletTypeDTO addWalletTypeFromDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteWalletById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<WalletDTO> GetAllWalletsByUserId(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
