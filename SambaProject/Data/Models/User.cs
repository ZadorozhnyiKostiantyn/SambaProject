using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SambaProject.Data.Models
{
    [Table("User")]
    public class User : IEntity
    {
        [Key]
        [Column("UserId")]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Password { get; set; } = null!;

        [ForeignKey("AccessRole")]
        public int AccessRoleId { get; set; }
        public virtual AccessRole AccessRole { get; set; }
    }
}
