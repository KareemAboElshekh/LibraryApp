using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibraryApp.DataAccess.Entities;

namespace LibraryApp.DataAccess.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            // المفتاح الرئيسي
            builder.HasKey(p => p.PermissionID);

            // عمود الاسم
            builder.Property(p => p.PermissionName)
                   .IsRequired()
                   .HasMaxLength(50);

            // علاقة واحد إلى متعدد مع RolePermissions
            builder.HasMany(p => p.RolePermissions)
                   .WithOne(rp => rp.Permission)
                   .HasForeignKey(rp => rp.PermissionID);
        }
    }
}
