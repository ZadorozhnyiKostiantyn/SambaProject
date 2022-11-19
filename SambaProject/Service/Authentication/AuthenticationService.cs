using Microsoft.AspNetCore.Identity;
using SambaProject.Data.Models;
using SambaProject.Data.Repository;
using SambaProject.Models;

namespace SambaProject.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGeneratorService _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAccessRoleRepository _accessRoleRepository;

        public AuthenticationService(
            IJwtTokenGeneratorService jwtTokenGenerator,
            IUserRepository userReporitory,
            IPasswordHasher<User> passwordHasher,
            IAccessRoleRepository accessRoleRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userReporitory;
            _passwordHasher = passwordHasher;
            _accessRoleRepository = accessRoleRepository;
        }

        public async Task Register(string username, string password, int roleId)
        {
            // 1. Validate the user doesn't exist
            if (await _userRepository.GetUserByUsernameAsync(username) is not null)
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
        }

        public async Task<AuthenticationResult> Login(string userName, string password)
        {
            // 1. Validate the user exists
            if (await _userRepository.GetUserByUsernameAsync(userName) is not User user)
            {
                throw new Exception("User with given username does not exist.");
            }

            // 2. Validate the password is correct
            if (_passwordHasher.VerifyHashedPassword(user, user.Password, password) != PasswordVerificationResult.Success)
            {
                throw new Exception("Invalid Password");
            }

            // 3. Create JWT Token
            var token = _jwtTokenGenerator.GenerateToken(user, _accessRoleRepository.GetAccessRoleById(user.AccessRoleId));

            return new AuthenticationResult(user, token);

        }
    }
}
