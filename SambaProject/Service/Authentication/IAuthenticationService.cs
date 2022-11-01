using SambaProject.Data.Models;

namespace SambaProject.Service.Authentication
{
    public interface IAuthenticationService
    {
        public Task Register(string userName, string password, int roleId);
        public Task<User> Login(string userName, string password);
    }
}
