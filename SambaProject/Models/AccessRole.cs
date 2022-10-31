using SambaProject.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SambaProject.Models
{
    public class AccessRole
    {
        [Key]
        public int AccessRoleId { get; set; }
        public AccessRolesCategories Role { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
