using SambaProject.Data.Models;

namespace SambaProject.Service.Authentication
{
    public interface IJwtTokenGeneratorService
    {
        string GenerateToken(User user, AccessRole role);
    }
}
