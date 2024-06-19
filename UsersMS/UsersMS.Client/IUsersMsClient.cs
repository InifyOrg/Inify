using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Contracts;

namespace UsersMS.Client
{
    interface IUsersMsClient
    {
        public Task<UserDTO> GetUserByID(long id);
        public Task<UserDTO> CreateUser(AddUserDTO userToAdd);
        public Task<UserDTO> EditUser(EditUserDTO userToAdd);
        public Task<UserDTO> EditUserPassword(EditUserPasswordDTO userToAdd);

    }
}
