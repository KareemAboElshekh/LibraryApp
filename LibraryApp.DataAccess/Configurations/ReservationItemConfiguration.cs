using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibraryApp.DataAccess.Entities;

namespace LibraryApp.DataAccess.Configurations
{
    public class ReservationItemConfiguration : IEntityTypeConfiguration<ReservationItem>
    {
        public void Configure(EntityTypeBuilder<ReservationItem> builder)
        {
            builder.ToTable("ReservationItems");
            builder.HasKey(ri => new { ri.ReservationId, ri.BookId });

            builder.Property(ri => ri.ReturnedDate);
            // ربط صريح للـ FK على Reservation
            builder.HasOne(ri => ri.Reservation)
                               .WithMany(r => r.Items)
                               .HasForeignKey(ri => ri.ReservationId)
                               .HasConstraintName("FK_ReservationItems_Reservations");
            
                        // ربط صريح للـ FK على Book
            builder.HasOne(ri => ri.Book)
                               .WithMany(b => b.ReservationItems)
                               .HasForeignKey(ri => ri.BookId)
                               .HasConstraintName("FK_ReservationItems_Books");
        }
    }
}