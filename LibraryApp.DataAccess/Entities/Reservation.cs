using System;
using System.Collections.Generic;

namespace LibraryApp.DataAccess.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "Borrowed";

        public byte[] RowVersion { get; set; } = null!;

        public User User { get; set; } = null!;
        public ICollection<ReservationItem> Items { get; set; }
            = new List<ReservationItem>();
    }
}
