using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibraryApp.DataAccess.Entities;

namespace LibraryApp.DataAccess.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");
            builder.HasKey(a => a.AuthorId);
            builder.Property(a => a.AuthorName)
                   .HasMaxLength(150)
                   .IsRequired();
            builder.Property(a => a.Biography).HasMaxLength(1000);
        }
    }
}