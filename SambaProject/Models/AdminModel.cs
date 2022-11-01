using SambaProject.Data.Models;

namespace SambaProject.Models
{
    public class AdminModel
    {
        public User User { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
