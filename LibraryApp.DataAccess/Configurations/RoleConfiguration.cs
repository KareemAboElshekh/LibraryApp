using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibraryApp.DataAccess.Entities;

namespace LibraryApp.DataAccess.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            // المفتاح الرئيسي
            builder.HasKey(r => r.RoleID);

            // عمود الاسم
            builder.Property(r => r.RoleName)
                   .IsRequired()
                   .HasMaxLength(50);

            // علاقة واحد إلى متعدد مع UserRoles
            builder.HasMany(r => r.UserRoles)
                   .WithOne(ur => ur.Role)
                   .HasForeignKey(ur => ur.RoleID);

            // علاقة واحد إلى متعدد مع RolePermissions
            builder.HasMany(r => r.RolePermissions)
                   .WithOne(rp => rp.Role)
                   .HasForeignKey(rp => rp.RoleID);
        }
    }
}

