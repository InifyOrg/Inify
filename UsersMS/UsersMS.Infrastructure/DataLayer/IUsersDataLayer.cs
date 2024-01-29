using UsersMS.Infrastructure.Domain.Entities;

namespace UsersMS.Infrastructure
{
    public interface IUsersDataLayer
    {
        Task<User> AddUser(User newuser);
        Task<User> GetUserById(long id);
    }
}