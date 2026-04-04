// BookReservationViewModel.cs
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryApp.BusinessLogic.Interfaces;
using LibraryApp.BusinessLogic.Models;
using static LibraryApp.BusinessLogic.Models.DTOs;

namespace LibraryApp.ViewModels.ViewModels
{
    public class BookReservationViewModel : ObservableObject
    {
        private readonly IBookService _bookService;
        private readonly IUserSessionService _session;

        // الكتب المتاحة للحجز
        public ObservableCollection<BookDto> AvailableBooks { get; } = new();

        private BookDto? selectedAvailableBook;
        public BookDto? SelectedAvailableBook
        {
            get => selectedAvailableBook;
            set
            {
                SetProperty(ref selectedAvailableBook, value);
                ReserveBookCommand.NotifyCanExecuteChanged();
            }
        }

        // الكتب المحجوزة للمستخدم الحالي
        public ObservableCollection<BookDto> ReservedBooks { get; } = new();

        private BookDto? selectedReservedBook;
        public BookDto? SelectedReservedBook
        {
            get => selectedReservedBook;
            set
            {
                SetProperty(ref selectedReservedBook, value);
                ReturnBookCommand.NotifyCanExecuteChanged();
            }
        }

        // الأوامر
        public IAsyncRelayCommand LoadBooksCommand { get; }
        public IAsyncRelayCommand ReserveBookCommand { get; }
        public IAsyncRelayCommand ReturnBookCommand { get; }

        public BookReservationViewModel(IBookService bookService, IUserSessionService session)
        {

            _bookService = bookService;
            _session = session;

            LoadBooksCommand = new AsyncRelayCommand(LoadBooksAsync);
            ReserveBookCommand = new AsyncRelayCommand(ReserveBookAsync, CanReserveBook);
            ReturnBookCommand = new AsyncRelayCommand(ReturnBookAsync, CanReturnBook);

            _ = LoadBooksAsync();
        }

        public async Task LoadBooksAsync()
        {
            var user = _session.CurrentUser;
            if (user is null)
                return;

            // 1) جلب كل الكتب المتاحة
            var available = (await _bookService.GetAvailableBooksAsync()).ToList();

            // 2) جلب كل الكتب المحجوزة لهذا المستخدم
            var reserved = (await _bookService.GetReservedBooksAsync(user.UserId)).ToList();

            // 3) تفريغ القوائم ثم تعبئتها
            AvailableBooks.Clear();
            ReservedBooks.Clear();

            // 4) أضف المحجوزة أولاً
            foreach (var b in reserved)
                ReservedBooks.Add(b);

            // 5) من قائمة المتاحة، استثنِ المحجوزة
            foreach (var b in available.Where(a => reserved.All(r => r.BookId != a.BookId)))
                AvailableBooks.Add(b);

            ReserveBookCommand.NotifyCanExecuteChanged();
            ReturnBookCommand.NotifyCanExecuteChanged();
        }


        private bool CanReserveBook() =>
            SelectedAvailableBook != null && _session.CurrentUser != null;

        public async Task ReserveBookAsync()
        {
            // 1) استخراج معرف المستخدم ومعرف الكتاب من DTO
            int userId = _session.CurrentUser!.UserId;
            int bookId = SelectedAvailableBook.BookId;

            // 2) استدعاء خدمة حجز الكتاب
            var result = await _bookService.AddReservationAsync(userId, bookId);

            // 3) تحديث القوائم في الواجهة حسب نتيجة العملية
            if (result.Success)
            {
                ReservedBooks.Add(SelectedAvailableBook);
                AvailableBooks.Remove(SelectedAvailableBook);
            }
        }
        private bool CanReturnBook() =>
            SelectedReservedBook != null && _session.CurrentUser != null;

        public async Task ReturnBookAsync()
        {
            if (SelectedReservedBook is null || _session.CurrentUser is null)
                return;

            var result = await _bookService.ReturnBookAsync(
                _session.CurrentUser.UserId,
                SelectedReservedBook.BookId
            );

            if (result.Success)
            {
                AvailableBooks.Add(SelectedReservedBook);
                ReservedBooks.Remove(SelectedReservedBook);
            }
        }
    }
}
