using SambaProject.Models;

namespace SambaProject.Service.Authentication.Interface
{
    public interface IJwtDecodingService
    {
        public UserModel DecodeToken(string token);
    }
}
