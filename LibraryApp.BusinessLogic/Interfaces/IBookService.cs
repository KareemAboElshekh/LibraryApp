using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.BusinessLogic.Models;
using LibraryApp.DataAccess.Entities;
using static LibraryApp.BusinessLogic.Models.DTOs;

namespace LibraryApp.BusinessLogic.Interfaces
{

    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAvailableBooksAsync();
        Task<IEnumerable<BookDto>> GetReservedBooksAsync(int userId);
        Task<ReservationResult> AddReservationAsync(int userId, int bookId);
        Task<ReturnResult> ReturnBookAsync(int userId, int bookId);

        // جلب جميع الكتب
        Task<IEnumerable<BookDto>> GetAllBooksAsync();

        // إنشاء كتاب جديد
        Task<OperationResult> CreateBookAsync(string bookName, int authorId, int pages, int publishYear);

        // حذف كتاب
        Task<OperationResult> DeleteBookAsync(int bookId);
       
    }



}

