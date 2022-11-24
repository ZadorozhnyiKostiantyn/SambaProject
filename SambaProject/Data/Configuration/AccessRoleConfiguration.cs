using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SambaProject.Data.Models;

namespace SambaProject.Data.Configuration
{
    public class AccessRoleConfiguration : IEntityTypeConfiguration<AccessRole>
    {
        public void Configure(EntityTypeBuilder<AccessRole> builder)
        {
            builder.HasKey(a => a.AccessRoleId);

            builder.Property(a => a.AccessRoleId)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(a => a.Role).IsRequired();
        }
    }
}
