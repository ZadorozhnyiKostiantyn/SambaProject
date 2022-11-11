using SambaProject.Models;

namespace SambaProject.Service.Authentication
{
    public interface IJwtDecodingService
    {
        public DecodingUserModel DecodeToken(string token);
    }
}
