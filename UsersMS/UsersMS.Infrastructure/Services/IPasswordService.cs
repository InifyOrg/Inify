namespace UsersMS.Infrastructure
{
    public interface IPasswordService
    {
        byte[] CreatePasswordHash(string password);
        bool ValidatePasswordAgainstHash(byte[] hashToCheck, string password);

    }
}