namespace SambaProject.Service.Authentication
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
