using SambaProject.Data.Models;

namespace SambaProject.Service.Authentication.Interface
{
    public interface IJwtTokenGeneratorService
    {
        string GenerateToken(User user, AccessRole role);
    }
}
