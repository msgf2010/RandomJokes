using System;
using System.Collections.Generic;
using System.Text;

namespace Class62Homework.Data
{
    public static class PasswordEncryption
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool PasswordMatch(string userInput, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(userInput, passwordHash);
        }
    }
}
