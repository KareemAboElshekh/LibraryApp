using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.BusinessLogic.Interfaces;
using LibraryApp.BusinessLogic.Models;
using LibraryApp.DataAccess;
using LibraryApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using static LibraryApp.BusinessLogic.Models.DTOs;

namespace LibraryApp.BusinessLogic.Services
{
    public class BookService : IBookService
    {
        private readonly IDbContextFactory<LibraryDbContext> _factory;
        public BookService(IDbContextFactory<LibraryDbContext> factory)
            => _factory = factory;

        public async Task<IEnumerable<BookDto>> GetAvailableBooksAsync()
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Books
                .Include(b => b.Author)
                .Where(b => !b.ReservationItems.Any(ri => ri.ReturnedDate == null))
                .Select(b => new BookDto
                {
                    BookId = b.BookId,
                    BookName = b.BookName,
                    AuthorName = b.Author.AuthorName
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<BookDto>> GetReservedBooksAsync(int userId)
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.ReservationItems
                .Include(ri => ri.Book!).ThenInclude(b => b.Author)
                .Include(ri => ri.Reservation)
                .Where(ri =>
                    ri.Reservation.UserId == userId &&
                    ri.ReturnedDate == null)
                .Select(ri => new BookDto
                {
                    BookId = ri.Book!.BookId,
                    BookName = ri.Book.BookName,
                    AuthorName = ri.Book.Author.AuthorName
                })
                .ToListAsync();
        }

        public async Task<ReservationResult> AddReservationAsync(int userId, int bookId)
        {
            await using var ctx = _factory.CreateDbContext();
            var reservation = new Reservation
            {
                UserId = userId,
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddDays(7),
                Status = "Borrowed"
            };
            reservation.Items.Add(new ReservationItem { BookId = bookId });
            ctx.Reservations.Add(reservation);
            await ctx.SaveChangesAsync();
            return new ReservationResult { Success = true, BookId = bookId };
        }

        public async Task<ReturnResult> ReturnBookAsync(int userId, int bookId)
        {
            await using var ctx = _factory.CreateDbContext();
            var item = await ctx.ReservationItems
                .Include(ri => ri.Reservation)
                .FirstOrDefaultAsync(ri =>
                    ri.Reservation.UserId == userId &&
                    ri.BookId == bookId &&
                    ri.ReturnedDate == null);

            if (item == null)
                return new ReturnResult { Success = false, Message = "لم يتم العثور على حجز مفتوح." };

            item.ReturnedDate = System.DateTime.Now;
            item.Reservation.Status = "Returned";
            await ctx.SaveChangesAsync();
            return new ReturnResult { Success = true, BookId = bookId };
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Books
                .Include(b => b.Author)
                .Select(b => new BookDto
                {
                    BookId = b.BookId,
                    BookName = b.BookName,
                    AuthorName = b.Author.AuthorName
                })
                .ToListAsync();
        }

        public async Task<OperationResult> CreateBookAsync(string bookName, int authorId, int pages, int publishYear)
        {
            await using var ctx = _factory.CreateDbContext();
            ctx.Books.Add(new Book
            {
                BookName = bookName,
                AuthorId = authorId,
                Pages = pages,
                PublishYear = publishYear
            });
            await ctx.SaveChangesAsync();
            return new OperationResult { Success = true };
        }

        public async Task<OperationResult> DeleteBookAsync(int bookId)
        {
            await using var ctx = _factory.CreateDbContext();
            var book = await ctx.Books.FindAsync(bookId);
            if (book == null)
                return new OperationResult { Success = false, Message = "الكتاب غير موجود." };

            ctx.Books.Remove(book);
            await ctx.SaveChangesAsync();
            return new OperationResult { Success = true };
        }
    }
}
