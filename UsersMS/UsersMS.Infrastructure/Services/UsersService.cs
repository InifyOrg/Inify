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

        public UsersService(IPasswordService passwordService, IUsersDataLayer usersDataLayer)
        {
            _passwordService = passwordService;
            _usersDataLayer = usersDataLayer;
        }

        public async Task<UserDTO> CreateUserFromDTO(AddUserDTO userToAdd)
        {
            User newuser = userToAdd.Adapt<User>();
            newuser.PasswordHash = _passwordService.CreatePasswordHash(userToAdd.Password);

            User addedUser = await _usersDataLayer.AddUser(newuser);

            UserDTO userDTO = addedUser.Adapt<UserDTO>();


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

    }
}
