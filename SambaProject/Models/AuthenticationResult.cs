using SambaProject.Data.Models;

namespace SambaProject.Models
{
    public record AuthenticationResult(
        User User,
        string Token);
}
