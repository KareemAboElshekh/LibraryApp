using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibraryApp.DataAccess.Entities;

namespace LibraryApp.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserName).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Password).HasMaxLength(200).IsRequired();
            builder.Property(u => u.Phone).HasMaxLength(20).IsRequired();
            builder.Property(u => u.RowVersion).IsRowVersion();

            builder.HasOne(u => u.Type)
                   .WithMany(t => t.Users)
                   .HasForeignKey(u => u.TypeId)
                   .HasConstraintName("FK_Users_Types");
        }
    }
}
