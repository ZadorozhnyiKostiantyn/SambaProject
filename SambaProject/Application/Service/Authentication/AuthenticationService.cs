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
