namespace SambaProject.Service.Authentication
{
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTime IDateTimeProvider.UtcNow => DateTime.UtcNow;
    }
}
