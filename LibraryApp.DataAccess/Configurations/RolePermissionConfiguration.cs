using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibraryApp.DataAccess.Entities;

namespace LibraryApp.DataAccess.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions");

            // المفتاح المركب
            builder.HasKey(rp => new { rp.RoleID, rp.PermissionID });

            // علاقة متعدد إلى واحد مع Role
            builder.HasOne(rp => rp.Role)
                   .WithMany(r => r.RolePermissions)
                   .HasForeignKey(rp => rp.RoleID);

            // علاقة متعدد إلى واحد مع Permission
            builder.HasOne(rp => rp.Permission)
                   .WithMany(p => p.RolePermissions)
                   .HasForeignKey(rp => rp.PermissionID);
        }
    }
}

