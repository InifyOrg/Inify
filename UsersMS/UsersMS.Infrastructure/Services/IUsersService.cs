using UsersMS.Contracts;

namespace UsersMS.Infrastructure
{
    public interface IUsersService
    {
        Task<UserDTO> CreateUserFromDTO(AddUserDTO userToAdd);
        Task<bool> EditUserFromDTO(EditUserDTO userToEdit);
        Task<UserDTO> GetUserById(long id);
    }
}