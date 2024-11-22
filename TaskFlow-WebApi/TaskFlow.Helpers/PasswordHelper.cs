using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFlow.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

    }
}