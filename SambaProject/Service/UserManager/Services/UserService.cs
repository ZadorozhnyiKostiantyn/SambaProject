using OneOf;
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
        private readonly IParseService _parseService;

        public UserService(
            IRepository<User> userRepository,
            IAuthenticationService authenticationService,
            IAccessRoleService accessRoleService,
            IJwtDecodingService jwtDecodingService,
            IHttpContextAccessor httpContextAccessor,
            IParseService parseService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _accessRoleService = accessRoleService;
            _jwtDecodingService = jwtDecodingService;
            _httpContextAccessor = httpContextAccessor;
            _parseService = parseService;
        }

        public async Task CreateUserAsync(string userName, string password, int roleId)
        {
            await _authenticationService.Register(
                userName: userName,
                password: password,
                roleId: roleId);
        }

        public async Task<OneOf<int, UserModel>> DeleteOwnerAsync(int ownerId)
        {
            await _userRepository.DeleteAsync(ownerId);
            if (await _userRepository.SingleOrDefaultAsync(u => u.AccessRoleId != 1) is null)
            {
                return ownerId;
            }

            if (await _userRepository.SingleOrDefaultAsync(u => u.AccessRoleId == 2) is User user)
            {
                return _parseService.ParseUserToUserModel(await UpdateUserAsync(
                    new User
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Password = user.Password,
                        AccessRoleId = 1

                    }));
            }
            return ownerId;
        }

        public async Task<OneOf<int, UserModel>> DeleteUserAsync(int userId)
        {
            if (_userRepository.GetById(userId)?.AccessRoleId != 1)
            {
                await _userRepository.DeleteAsync(userId);
                return userId;
            }
            return await DeleteOwnerAsync(userId);
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

            if (!string.IsNullOrEmpty(query))
            {
                return _parseService
                    .ParseUserToUserModel(await _userRepository.SearchAsync(u => u.Username!.Contains(query)));
            }

            return _parseService.ParseUserToUserModel(await _userRepository.GetAllAsync());

        }

        public async Task<User> UpdateUserAsync(User newData)
        {
             return await _userRepository.UpdateAsync(newData);
        }
    }
}
