namespace SambaProject.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string AccessRole { get; set; } = null!;
    }
}
