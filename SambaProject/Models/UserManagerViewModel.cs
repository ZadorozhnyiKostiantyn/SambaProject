using SambaProject.Data.Models;

namespace SambaProject.Models
{
    public class UserManagerViewModel
    {
        public User User { get; set; }
        public IEnumerable<UserModel> Users { get; set; }
        public IEnumerable<AccessRole> AccessRoles { get; set; }
    }
}
