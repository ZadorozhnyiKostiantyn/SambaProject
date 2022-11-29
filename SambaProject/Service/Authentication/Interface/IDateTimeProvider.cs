namespace SambaProject.Service.Authentication.Interface
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
