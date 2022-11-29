namespace SambaProject.Models
{
    public class AccessRoleClaim
    {
        public string Type { get; set; } = null!;
        public List<string> Value { get; set; } = null!;
    }
}
