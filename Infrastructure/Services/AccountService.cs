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
using ApplicationCore.Entities;

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

            var salt = GetRandomSalt();

            // hash the password with the salt created in above step
            var hashedPassword = GetHashedPassword(model.Password, salt);

            // create new User entity object and save it to database using EF Core SaveChanges method
            var dbUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                DateOfBirth = model.DateOfBirth
            };

            var createdUser = await _userRepository.AddUser(dbUser);
            return createdUser.Id;
        }

        public async Task<UserLoginSuccessModel> ValidateUser(UserLoginModel model)
        {
            var user = await _userRepository.GetUserByEmail(model.Email);
            if (user == null)
            {
                throw new Exception("Email does not exixts, try to register first");
            }

            var hashedPassword = GetHashedPassword(model.Password, user.Salt);

            if (user.HashedPassword == hashedPassword)
            {
                var userLoginSuccessModel = new UserLoginSuccessModel
                {
                    Id = user.Id, 
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            return userLoginSuccessModel;
            }

            return null;
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
