using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibraryApp.DataAccess.Entities;

namespace LibraryApp.DataAccess.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(b => b.BookId);
            builder.Property(b => b.BookName).HasMaxLength(200).IsRequired();
            builder.Property(b => b.Pages).IsRequired();
            builder.Property(b => b.PublishYear).IsRequired();
            builder.Property(b => b.RowVersion).IsRowVersion();

            builder.HasOne(b => b.Author)
                   .WithMany(a => a.Books)
                   .HasForeignKey(b => b.AuthorId)
                   .HasConstraintName("FK_Books_Authors");

            builder.HasMany(b => b.ReservationItems)
                   .WithOne(ri => ri.Book)
                   .HasForeignKey(ri => ri.BookId)
                   .HasConstraintName("FK_ReservationItems_Books");
        }
    }
}
