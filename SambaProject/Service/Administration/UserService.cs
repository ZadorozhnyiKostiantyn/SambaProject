using SambaProject.Data.Models;
using SambaProject.Data.Repository;
using SambaProject.Service.Authentication;

namespace SambaProject.Service.Administration
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public UserService(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
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

        public async Task<List<User>> SearchAsync(string query)
        {
            // 1. Parsing query
            /*
            {
                Username: username,
                AccessRole: accseerole
            };
             */
            if(query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            // 2. Get list all users
            var listUser = await _userRepository.GetAllUserAsync();

            // 3. Generate a list of users for a suitable request
            var findListUsers = listUser;

            return findListUsers;

        }

        public async Task UpdateUserAsync(User newData)
        {
            await _userRepository.UpdateUserAsync(newData: newData);
        }
    }
}
