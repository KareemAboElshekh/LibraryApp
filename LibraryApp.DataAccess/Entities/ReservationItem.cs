using System;

namespace LibraryApp.DataAccess.Entities
{
    public class ReservationItem
    {
        public int ReservationId { get; set; }
        public int BookId { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public Reservation Reservation { get; set; } = null!;
        public Book Book { get; set; } = null!;
    }
}
