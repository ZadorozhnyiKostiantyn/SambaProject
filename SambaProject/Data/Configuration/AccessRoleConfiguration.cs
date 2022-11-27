using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SambaProject.Data.Models;

namespace SambaProject.Data.Configuration
{
    public class AccessRoleConfiguration : IEntityTypeConfiguration<AccessRole>
    {
        public void Configure(EntityTypeBuilder<AccessRole> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(a => a.Role).IsRequired();
        }
    }
}
