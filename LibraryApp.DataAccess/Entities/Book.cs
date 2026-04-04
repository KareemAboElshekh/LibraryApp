using System.Collections.Generic;

namespace LibraryApp.DataAccess.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; } = null!;
        public int Pages { get; set; }
        public int PublishYear { get; set; }

        // FK إلى Author
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        public byte[] RowVersion { get; set; } = null!;

        public ICollection<ReservationItem> ReservationItems { get; set; }
            = new List<ReservationItem>();
    }
}
