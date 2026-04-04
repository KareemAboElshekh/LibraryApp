using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibraryApp.DataAccess.Entities;

namespace LibraryApp.DataAccess.Configurations
{
    public class TypeConfiguration : IEntityTypeConfiguration<Type>
    {
        public void Configure(EntityTypeBuilder<Type> builder)
        {
            builder.ToTable("Types");
            builder.HasKey(t => t.TypeId);
            builder.Property(t => t.TypeName)
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
