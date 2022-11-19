using SambaProject.Models;

namespace SambaProject.Service.Authentication
{
    public interface IJwtDecodingService
    {
        public UserModel DecodeToken(string token);
    }
}
