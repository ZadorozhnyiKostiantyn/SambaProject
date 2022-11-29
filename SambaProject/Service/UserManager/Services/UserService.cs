using AutoMapper;
using SambaProject.Data;
using SambaProject.Data.Models;
using SambaProject.Data.Repository;
using SambaProject.Models;
using SambaProject.Service.Authentication.Interface;
using SambaProject.Service.UserManager.Interface;

namespace SambaProject.Service.UserManager.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccessRoleService _accessRoleService;
        private readonly IJwtDecodingService _jwtDecodingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            IRepository<User> userRepository,
            IAuthenticationService authenticationService,
            IAccessRoleService accessRoleService,
            IJwtDecodingService jwtDecodingService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _accessRoleService = accessRoleService;
            _jwtDecodingService = jwtDecodingService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateUserAsync(string userName, string password, int roleId)
        {
            await _authenticationService.Register(
                userName: userName,
                password: password,
                roleId: roleId);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _userRepository.DeleteAsync(userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public UserModel GetUserByToken()
        {
            return _jwtDecodingService.DecodeToken(_httpContextAccessor.HttpContext.Session?.GetString("Token"));
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<List<UserModel>> SearchAsync(string query)
        {
            List<User> users;

            if (string.IsNullOrEmpty(query))
            {
                users = await _userRepository.GetAllAsync();
            }
            else
            {
                users = await _userRepository.SearchAsync(u => u.Username!.Contains(query));
            }

            List<UserModel> result = new List<UserModel>();

            foreach (var user in users)
            {
                result.Add(
                    new UserModel
                    {
                        Id = user.Id,
                        Username = user.Username,
                        AccessRole = _accessRoleService.GetRoleById(user.AccessRoleId).Role
                    }
                );
            }

            return result;

        }

        public async Task UpdateUserAsync(User newData)
        {
            await _userRepository.Update(newData);
        }
    }
}
