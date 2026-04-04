using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibraryApp.DataAccess.Entities;

namespace LibraryApp.DataAccess.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");
            builder.HasKey(r => r.ReservationId);
            builder.Property(r => r.Status).HasMaxLength(50).IsRequired();
            builder.Property(r => r.RowVersion).IsRowVersion();

            builder.HasOne(r => r.User)
                   .WithMany(u => u.Reservations)
                   .HasForeignKey(r => r.UserId)
                   .HasConstraintName("FK_Reservations_Users");

            builder.HasMany(r => r.Items)
                   .WithOne(ri => ri.Reservation)
                   .HasForeignKey(ri => ri.ReservationId)
                   .HasConstraintName("FK_ReservationItems_Reservations");
        }
    }
}
