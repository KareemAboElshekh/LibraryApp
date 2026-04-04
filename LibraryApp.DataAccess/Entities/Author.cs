using System.Collections.Generic;

namespace LibraryApp.DataAccess.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = null!;
        public string? Biography { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
