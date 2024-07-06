using UsersMS.Contracts;

namespace UsersMS.Infrastructure
{
    public interface IUsersService
    {
        Task<UserDTO> CreateUserFromDTO(AddUserDTO userToAdd);
        Task<bool> EditUserFromDTO(EditUserDTO userToEdit);
        Task<bool> EditUserPasswordFromDTO(EditUserPasswordDTO userPasswordToEdit);
        Task<UserDTO> GetUserById(long id);
        Task<UserDTO> GetUserByEmail(string email);
        Task<AccessTokenDTO> LoginFromDTO(LoginDTO loginDTO);
    }
}