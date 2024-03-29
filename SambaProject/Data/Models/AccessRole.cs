﻿using Syncfusion.EJ2.FileManager.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SambaProject.Data.Models
{
    [Table("Access Role")]
    public class AccessRole : IEntity
    {
        [Key]
        [Column("AccessRoleId")]
        public int Id { get; set; }
        public string Role { get; set; } = null!;
        public virtual ICollection<AccessRuleRoles> Rules { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
