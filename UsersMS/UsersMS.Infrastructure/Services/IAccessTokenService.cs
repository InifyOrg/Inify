using UsersMS.Infrastructure.Domain.Entities;

namespace UsersMS.Infrastructure
{
    public interface IAccessTokenService
    {
        string GenerateAccessTokenFromUser(User userByEmail);
        bool ValidateAccessToken(string jwtToken);
    }
}