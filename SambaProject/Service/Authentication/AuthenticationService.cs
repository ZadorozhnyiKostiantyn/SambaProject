using Microsoft.AspNetCore.Identity;
using SambaProject.Data.Models;
using SambaProject.Data.Repository;
using SambaProject.Models;

namespace SambaProject.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGeneratorService _jwtTokenGenerator;
        private readonly IRepository<User> _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<AccessRole> _accessRoleRepository;

        public AuthenticationService(
            IJwtTokenGeneratorService jwtTokenGenerator,
            IRepository<User> userReporitory,
            IPasswordHasher<User> passwordHasher,
            IRepository<AccessRole> accessRoleRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userReporitory;
            _passwordHasher = passwordHasher;
            _accessRoleRepository = accessRoleRepository;
        }

        public async Task Register(string username, string password, int roleId)
        {
            // 1. Validate the user doesn't exist
            if (await _userRepository.SingleOrDefaultAsync(u => u.Username == username) is not null)
            {
                throw new Exception("User with given username already exists.");
            }

            // 2. Creat user (generate unique ID) and Persist to DB
            var user = new User
            {
                Username = username,
                AccessRoleId = roleId,
            };
            user.Password = _passwordHasher.HashPassword(user, password);

            await _userRepository.AddAsync(user);
        }

        public async Task<AuthenticationResult> Login(string username, string password)
        {
            // 1. Validate the user exists
            if (await _userRepository.SingleOrDefaultAsync(u => u.Username == username) is not User user)
            {
                throw new Exception("User with given username does not exist.");
            }

            // 2. Validate the password is correct
            if (_passwordHasher.VerifyHashedPassword(user, user.Password, password) != PasswordVerificationResult.Success)
            {
                throw new Exception("Invalid Password");
            }

            // 3. Create JWT Token
            var token = _jwtTokenGenerator.GenerateToken(user, _accessRoleRepository.GetById(user.AccessRoleId));

            return new AuthenticationResult(user, token);

        }
    }
}
