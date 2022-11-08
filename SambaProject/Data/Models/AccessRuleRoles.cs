using Syncfusion.EJ2.FileManager.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SambaProject.Data.Models
{
    public class AccessRuleRoles
    {
        [Key]
        public int AccessRuleRolesId { get; set; }

        [Required]
        public string Path { get; set; } = null!;

        public Permission Copy { get; set; }

        public Permission Download { get; set; }

        public Permission Write { get; set; }

        public Permission Read { get; set; }

        public Permission WriteContents { get; set; }

        public Permission Upload { get; set; }

        public bool IsFile { get; set; }

        [ForeignKey("AccessRole")]
        public int AccessRoleId { get; set; }
        public virtual AccessRole AccessRole { get; set; }

    }
}
