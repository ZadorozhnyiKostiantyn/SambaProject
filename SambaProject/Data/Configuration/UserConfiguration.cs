using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SambaProject.Data.Models;

namespace SambaProject.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.AccessRoleId).IsRequired();

            builder
                .HasOne(a => a.AccessRole)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.AccessRoleId);

        }
    }
}
