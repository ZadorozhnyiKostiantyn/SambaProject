namespace SambaProject.Service.Authentication
{
    public interface IJwtValidatorService
    {
        bool ValidateToken(string token);
    }
}
