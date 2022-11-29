using SambaProject.Service.Authentication.Interface;

namespace SambaProject.Service.Authentication.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTime IDateTimeProvider.UtcNow => DateTime.UtcNow;
    }
}
