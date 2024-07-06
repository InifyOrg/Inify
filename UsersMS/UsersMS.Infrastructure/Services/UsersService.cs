using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Contracts;
using UsersMS.Infrastructure.Domain.Entities;

namespace UsersMS.Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly IPasswordService _passwordService;
        private readonly IUsersDataLayer _usersDataLayer;
        private readonly IAccessTokenService _accessTokenService;

        public UsersService(IPasswordService passwordService, IUsersDataLayer usersDataLayer, IAccessTokenService accessTokenService)
        {
            _passwordService = passwordService;
            _usersDataLayer = usersDataLayer;
            _accessTokenService = accessTokenService;
        }

        public async Task<UserDTO> CreateUserFromDTO(AddUserDTO userToAdd)
        {
            User newuser = userToAdd.Adapt<User>();
            newuser.PasswordHash = _passwordService.CreatePasswordHash(userToAdd.Password);

            User addedUser = await _usersDataLayer.AddUser(newuser);

            return addedUser.Adapt<UserDTO>();

        }

        public async Task<UserDTO> GetUserById(long id)
        {
            User userById = await _usersDataLayer.GetUserById(id);

            UserDTO userDTO = userById.Adapt<UserDTO>();

            return userDTO;
        }

        public async Task<bool> EditUserFromDTO(EditUserDTO userToEdit)
        {
            if (userToEdit.Id > 0)
            {
                if (userToEdit.IsUpdated())
                {

                    User userById = await _usersDataLayer.GetUserById(userToEdit.Id);

                    if (userById != null)
                    {

                        userById.UpdateFromEditUserDto(userToEdit);

                        return await _usersDataLayer.Edit(userById);
                    }
                }
            }

            return false;
        }

        public async Task<bool> EditUserPasswordFromDTO(EditUserPasswordDTO userPasswordToEdit)
        {
            if (userPasswordToEdit.Id > 0)
            {
                User userById = await _usersDataLayer.GetUserById(userPasswordToEdit.Id);

                if (_passwordService.ValidatePasswordAgainstHash(userById.PasswordHash, userPasswordToEdit.OldPassword))
                {
                    userById.PasswordHash = _passwordService.CreatePasswordHash(userPasswordToEdit.NewPassword);

                    return await _usersDataLayer.Edit(userById);
                }
            }
            return false;
        }

        public async Task<AccessTokenDTO> LoginFromDTO(LoginDTO loginDTO)
        {
            AccessTokenDTO accessTokenDTO = new AccessTokenDTO();
            User userByEmail = await _usersDataLayer.GetUserByEmail(loginDTO.Email);

            if (userByEmail.Id > 0 && _passwordService.ValidatePasswordAgainstHash(userByEmail.PasswordHash, loginDTO.Password)) 
            {
                string jwtToken = _accessTokenService.GenerateAccessTokenFromUser(userByEmail);
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    accessTokenDTO.Token = jwtToken;

                    AccessToken newAccessToken = accessTokenDTO.Adapt<AccessToken>();
                    AccessToken addedAccessToken = await _usersDataLayer.AddAccessToken(newAccessToken, userByEmail.Id);

                    accessTokenDTO = addedAccessToken.Adapt<AccessTokenDTO>();
                }
            }
            
            return accessTokenDTO;
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            User userByEmail = await _usersDataLayer.GetUserByEmail(email);

            UserDTO userDTO = userByEmail.Adapt<UserDTO>();

            return userDTO;
        }
    }
}
