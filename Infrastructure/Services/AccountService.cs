using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> RegisterUser(UserRegisterModel model)
        {
            // check if the email exists in the database
            // go to user table using UserRepository
            var user = await _userRepository.GetUserByEmail(model.Email);
            if (user != null)
            {
                throw new Exception("Email already exists, try to login");
            }

            // create a random salt
            // hash the password with the salt created in above step
            // create new User entity object and save it to database using EF Core SaveChanges method


        }

        public Task<UserLoginSuccessModel> ValidateUser(UserLoginModel model)
        {
            throw new NotImplementedException();
        }


        private string GetRandomSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
           password,
           Convert.FromBase64String(salt),
           KeyDerivationPrf.HMACSHA512,
           10000,
           256 / 8));
            return hashed;
        }
    }
}
