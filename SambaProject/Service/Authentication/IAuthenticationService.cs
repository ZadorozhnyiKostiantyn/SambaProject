using SambaProject.Data.Models;
using SambaProject.Models;

namespace SambaProject.Service.Authentication
{
    public interface IAuthenticationService
    {
        public Task Register(string userName, string password, int roleId);
        public Task<AuthenticationResult> Login(string userName, string password);
    }
}
