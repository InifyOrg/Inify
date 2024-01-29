using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UsersMS.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        public Byte[] CreatePasswordHash(string openTextPassword)
        {
            SHA256 hasher = SHA256.Create();

            return hasher.ComputeHash(Encoding.UTF8.GetBytes(openTextPassword));
        }

        public bool ValidatePasswordAgainstHash(byte[] hashToCheck, string password)
        {
            byte[] hashOfPasswordToCheck = CreatePasswordHash(password);

            if (hashToCheck.Length != hashOfPasswordToCheck.Length) return false;

            for (int i = 0; i < hashOfPasswordToCheck.Length; i++)
            {
                if (hashToCheck[i] != hashOfPasswordToCheck[i])
                    return false;
            }
            return true;
        }
    }
}
