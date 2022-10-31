using SambaProject.Data.Enum;
using SambaProject.Models;

namespace SambaProject.Application.Service.Authentication
{
    public interface IAuthenticationService
    {
        public Task<User> Login(string userName, string password);
    }
}
