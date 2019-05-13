using System;
using System.Collections.Generic;
using System.Text;
using BCrypt.Net;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Class62Homework.Data
{
    public class Authentication
    {
        private readonly string _connectionString;

        public Authentication(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user, string password)
        {
            user.Password = PasswordEncryption.HashPassword(password);

            using (var context = new JokesContext(_connectionString))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public User GetByEmail(string email)
        {
            using (var context = new JokesContext(_connectionString))
            {
                return context.Users.FirstOrDefault(u => u.Email == email);
            }
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            bool isCorrectPassword = PasswordEncryption.PasswordMatch(password, user.Password);
            if (isCorrectPassword)
            {
                return user;
            }

            return null;
        }
    }
}
