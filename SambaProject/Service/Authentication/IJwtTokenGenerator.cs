using SambaProject.Data.Models;

namespace SambaProject.Service.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
