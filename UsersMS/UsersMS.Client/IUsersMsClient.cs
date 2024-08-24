using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Contracts;

namespace UsersMS.Client
{
    public interface IUsersMsClient
    {
        public Task<UserDTO> GetUserByID(long id);
        public Task<UserDTO> GetUserByEmail(string email);
        public Task<UserDTO> Register(AddUserDTO userToAdd);
        public Task<string> Login(LoginDTO loginDTO);
        public Task<bool> EditUser(EditUserDTO userToAdd);
        public Task<bool> EditUserPassword(EditUserPasswordDTO userToAdd);
        public Task<bool> ValidateAccessToken(string accessToken);
    }
}
