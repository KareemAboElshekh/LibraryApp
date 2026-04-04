using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.DataAccess.Entities;

namespace LibraryApp.BusinessLogic.Models
{

    public static class DTOs
    {
        public class BookDto
        {
            public int BookId { get; set; }
            public string BookName { get; set; } = string.Empty;
            public string AuthorName { get; set; } = string.Empty;
        }

        public class UserDto
        {
            public int UserId { get; set; }
            public string Username { get; set; } = string.Empty;
            public string TypeName { get; set; } = string.Empty;
        }

        public class ReservationResult
        { 
            public bool Success { get; set; }

            public int BookId { get; set; }

            public string Message { get; set; } = string.Empty;
        }
        public class ReturnResult 
        {
            public bool Success { get; set; }
            public int BookId { get; set; }
            public string Message { get; set; } = string.Empty;
        }

    }
}
