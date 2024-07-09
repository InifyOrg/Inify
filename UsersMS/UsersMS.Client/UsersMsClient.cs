using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UsersMS.Contracts;

namespace UsersMS.Client
{
    public class UsersMsClient : IUsersMsClient
    {
        private readonly string? _serviceAddress;
        private readonly string? _apiBaseAddress;
        private bool _configurationReady;
        private readonly HttpClient _httpClient;

        public UsersMsClient()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("clientsettings.json")
                .Build();

            _serviceAddress = configuration.GetSection("UserServiceAddress").Value;
            _apiBaseAddress = configuration.GetSection("UserServiceApiBase").Value;
            _configurationReady = !string.IsNullOrEmpty(_apiBaseAddress) && !string.IsNullOrEmpty(_serviceAddress);
            _httpClient = new HttpClient();
        }

        public async Task<UserDTO> CreateUser(AddUserDTO userToAdd)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditUser(EditUserDTO userToAdd)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditUserPassword(EditUserPasswordDTO userToAdd)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetUserByID(long id)
        {
            UserDTO userById = new UserDTO();

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/getUserById/{id}";

                HttpResponseMessage userMsResponce = await _httpClient.GetAsync(requestAddress);

                string jsonUser = await userMsResponce.Content.ReadAsStringAsync();

                userById = JsonSerializer.Deserialize<UserDTO>(jsonUser);
                return userById;
            }

            return userById;
        }

        public async Task<bool> ValidateAccessToken(string accessToken)
        {

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/validateLogin/{accessToken}";

                HttpResponseMessage userMsResponce = await _httpClient.GetAsync(requestAddress);

                string json = await userMsResponce.Content.ReadAsStringAsync();

                return bool.Parse(json);
            }

            return false;
        }
    }
}
