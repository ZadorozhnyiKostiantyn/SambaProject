using OneOf;
using SambaProject.Data.Models;
using SambaProject.Models;

namespace SambaProject.Service.UserManager.Interface
{
    public interface IUserService
    {
        public Task CreateUserAsync(string username, string password, int roleId);
        public Task<List<User>> GetAllUsersAsync();
        public Task<User> UpdateUserAsync(User newData);
        public Task<OneOf<int, UserModel>> DeleteUserAsync(int userId);
        public Task<OneOf<int, UserModel>> DeleteOwnerAsync(int ownerId);
        public Task<List<UserModel>> SearchAsync(string query);
        public Task<User?> GetUserByIdAsync(int userId);
        public Task<User?> GetUserByUsernameAsync(string username);
        public UserModel GetUserByToken();
    }
}
