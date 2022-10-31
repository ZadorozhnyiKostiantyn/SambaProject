using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SambaProject.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        [StringLength(255)]
        public string Password { get; set; } = null!;

        [ForeignKey("AccessRoleModel")]
        public int AccessRoleId { get; set; }
        public virtual AccessRole AccessRole { get; set; }
    }
}
