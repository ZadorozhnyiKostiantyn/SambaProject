using SambaProject.Data.Models;
using SambaProject.Models;

namespace SambaProject.Service.UserManager
{
    public interface IUserService
    {
        public Task CreateUserAsync(string username, string password, int roleId);
        public Task<List<User>> GetAllUsersAsync();
        public Task UpdateUserAsync(User newData);
        public Task DeleteUserAsync(int userId);
        public Task<List<UserModel>> SearchAsync(string query);
        public Task<User> GetUserByIdAsync(int userId);
        public Task<User> GetUserByUsernameAsync(string username);
    }
}
