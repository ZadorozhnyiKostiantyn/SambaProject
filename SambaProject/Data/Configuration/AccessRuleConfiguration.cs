using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SambaProject.Data.Models;

namespace SambaProject.Data.Configuration
{
    public class AccessRuleConfiguration : IEntityTypeConfiguration<AccessRuleRoles>
    {
        public void Configure(EntityTypeBuilder<AccessRuleRoles> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Path).IsRequired();
            builder.Property(a => a.Copy).IsRequired();
            builder.Property(a => a.Download).IsRequired();
            builder.Property(a => a.Write).IsRequired();
            builder.Property(a => a.Read).IsRequired();
            builder.Property(a => a.WriteContents).IsRequired();
            builder.Property(a => a.Upload).IsRequired();
            builder.Property(a => a.Copy).IsRequired();
            builder.Property(a => a.IsFile).IsRequired();



            builder
                .HasOne(a => a.AccessRole)
                .WithMany(a => a.Rules)
                .HasForeignKey(u => u.AccessRoleId);
        }
    }
}
