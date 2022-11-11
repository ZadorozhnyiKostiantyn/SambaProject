namespace SambaProject.Models
{
    public class DecodingUserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string AccessRole { get; set; } = null!;
    }
}
