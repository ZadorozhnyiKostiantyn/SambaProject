using SambaProject.Data;
using SambaProject.Data.Models;
using SambaProject.Data.Repository;
using SambaProject.Models;
using SambaProject.Service.Authentication;

namespace SambaProject.Service.UserManager
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccessRoleService _accessRoleService;

        public UserService(
            IUserRepository userRepository,
            IAuthenticationService authenticationService,
            IAccessRoleService accessRoleService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _accessRoleService = accessRoleService;
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
            await _userRepository.DeleteUserAsync(userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUserAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<List<UserModel>> SearchAsync(string query)
        {
            var users = await _userRepository.SearchUsersAsync(query);

            List<UserModel> result = new List<UserModel>();

            foreach (var user in users)
            {
                result.Add(
                    new UserModel
                    {
                        Id = user.UserId,
                        Username = user.Username,
                        AccessRole = _accessRoleService.GetRoleById(user.AccessRoleId).Role
                    }
                );
            }

            return result;

        }

        public async Task UpdateUserAsync(User newData)
        {
            await _userRepository.UpdateUserAsync(newData);
        }
    }
}
