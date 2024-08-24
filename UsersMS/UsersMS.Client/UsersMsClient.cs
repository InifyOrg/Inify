using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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
                .AddJsonFile("usersclientsettings.json")
                .Build();

            _serviceAddress = configuration.GetSection("UserServiceAddress").Value;
            _apiBaseAddress = configuration.GetSection("UserServiceApiBase").Value;
            _configurationReady = !string.IsNullOrEmpty(_apiBaseAddress) && !string.IsNullOrEmpty(_serviceAddress);
            _httpClient = new HttpClient();
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

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            UserDTO user = new UserDTO();

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/getUserByEmail/{email}";

                HttpResponseMessage userMsResponce = await _httpClient.GetAsync(requestAddress);

                string jsonUser = await userMsResponce.Content.ReadAsStringAsync();

                user = JsonSerializer.Deserialize<UserDTO>(jsonUser);
            }

            return user;
        }

        public async Task<UserDTO> Register(AddUserDTO userToAdd)
        {
            UserDTO user = new UserDTO();

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/register";
                HttpContent httpContent = JsonContent.Create(userToAdd);

                HttpResponseMessage userMsResponce = await _httpClient.PostAsync(requestAddress, httpContent);

                string jsonUser = await userMsResponce.Content.ReadAsStringAsync();

                user = JsonSerializer.Deserialize<UserDTO>(jsonUser);
            }

            return user;
        }

        public async Task<string> Login(LoginDTO loginDTO)
        {

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/login";
                HttpContent httpContent = JsonContent.Create(loginDTO);

                HttpResponseMessage userMsResponce = await _httpClient.PostAsync(requestAddress, httpContent);

                string jsonUser = await userMsResponce.Content.ReadAsStringAsync();
                
                return jsonUser;
            }

            return "";
        }

        public async Task<bool> EditUser(EditUserDTO userToAdd)
        {

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/editUser";
                HttpContent httpContent = JsonContent.Create(userToAdd);

                HttpResponseMessage userMsResponce = await _httpClient.PutAsync(requestAddress, httpContent);

                string json = await userMsResponce.Content.ReadAsStringAsync();

                return bool.Parse(json);
            }

            return false;
        }

        public async Task<bool> EditUserPassword(EditUserPasswordDTO userToAdd)
        {

            if (_configurationReady)
            {
                string requestAddress = $"{_serviceAddress}/{_apiBaseAddress}/editPassword";
                HttpContent httpContent = JsonContent.Create(userToAdd);

                HttpResponseMessage userMsResponce = await _httpClient.PutAsync(requestAddress, httpContent);

                string json = await userMsResponce.Content.ReadAsStringAsync();

                return bool.Parse(json);
            }

            return false;
        }
    }
}
