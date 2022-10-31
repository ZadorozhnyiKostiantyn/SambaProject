using Microsoft.AspNetCore.Identity;
using SambaProject.Application.Common.Interfaces.Authentication;
using SambaProject.Application.Common.Interfaces.Persistence;
using SambaProject.Models;

namespace SambaProject.Application.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userReporitory, IPasswordHasher<User> passwordHasher)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userReporitory;
            _passwordHasher = passwordHasher;
        }

        public async Task Register(string username, string password, int roleId)
        {
            // 1. Validate the user doesn't exist
            if (await _userRepository.GetUserByUserNameAsync(username) is not null)
            {
                throw new Exception("User with given username already exists.");
            }

            // 2. Creat user (generate unique ID) and Persist to DB
            var user = new User
            {
                Username = username,
                Password = password,
                AccessRoleId = roleId,
            };
            user.Password = _passwordHasher.HashPassword(user, password);

            await _userRepository.AddUserAsync(user);

            // 3. Create JWT token
        }

        public async Task<User> Login(string userName, string password)
        {
            // 1. Validate the user exists
            if (await _userRepository.GetUserByUserNameAsync(userName) is not User user)
            {
                throw new Exception("User with given username does not exist.");
            }

            // 2. Validate the password is correct
            if (_passwordHasher.VerifyHashedPassword(user, user.Password, password) != PasswordVerificationResult.Success)
            {
                throw new Exception("Invalid Password");
            }

            // 3. Create JWT Token

            return user;

        }
    }
}
