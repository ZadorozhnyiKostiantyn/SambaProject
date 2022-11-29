using ErrorOr;
using SambaProject.Data.Models;
using SambaProject.Models;

namespace SambaProject.Service.Authentication.Interface
{
    public interface IAuthenticationService
    {
        public Task<ErrorOr<AuthenticationResult>> Register(string userName, string password, int roleId);
        public Task<ErrorOr<AuthenticationResult>> Login(string userName, string password);
    }
}
