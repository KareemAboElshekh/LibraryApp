using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryApp.BusinessLogic.Interfaces;
using LibraryApp.BusinessLogic.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static LibraryApp.BusinessLogic.Models.DTOs;

namespace LibraryApp.ViewModels.ViewModels
{
    public partial class BookManagementViewModel : ObservableObject
    {
        private readonly IBookService _bookService;

        public ObservableCollection<BookDto> Books { get; } = new();

        [ObservableProperty]
        private BookDto? selectedBook;

        [ObservableProperty]
        private string message = "";

        // حقول الإدخال
        [ObservableProperty] private string newBookName = "";
        [ObservableProperty] private int newAuthorId;
        [ObservableProperty] private int newPages;
        [ObservableProperty] private int newPublishYear;

        public IAsyncRelayCommand LoadBooksCommand { get; }
        public IAsyncRelayCommand AddBookCommand { get; }
        public IAsyncRelayCommand DeleteBookCommand { get; }

        public BookManagementViewModel(IBookService bookService)
        {
            _bookService = bookService;

            LoadBooksCommand = new AsyncRelayCommand(LoadBooksAsync);
            AddBookCommand = new AsyncRelayCommand(AddBookAsync, CanAddBook);
            DeleteBookCommand = new AsyncRelayCommand(DeleteBookAsync, () => SelectedBook != null);

            _ = LoadBooksAsync();
        }

        private async Task LoadBooksAsync()
        {
            Books.Clear();
            var list = await _bookService.GetAllBooksAsync();
            foreach (var b in list) Books.Add(b);

            // أعد حساب صلاحيات الأوامر
            AddBookCommand.NotifyCanExecuteChanged();
            DeleteBookCommand.NotifyCanExecuteChanged();
        }

        private bool CanAddBook() =>
            !string.IsNullOrWhiteSpace(NewBookName) &&
            NewAuthorId > 0 &&
            NewPages > 0 &&
            NewPublishYear > 0;

        private async Task AddBookAsync()
        {
            var result = await _bookService.CreateBookAsync(
                NewBookName, NewAuthorId, NewPages, NewPublishYear
            );
            Message = result.Success ? "تم الإضافة" : $"خطأ: {result.Message}";
            if (result.Success)
            {
                await LoadBooksAsync();
                NewBookName = "";
                NewAuthorId = 0;
                NewPages = 0;
                NewPublishYear = 0;
            }
            AddBookCommand.NotifyCanExecuteChanged();
        }

        private async Task DeleteBookAsync()
        {
            if (SelectedBook == null) return;

            var result = await _bookService.DeleteBookAsync(SelectedBook.BookId);
            Message = result.Success ? "تم الحذف" : $"خطأ: {result.Message}";
            await LoadBooksAsync();
        }

        // هذا partial method يُنفّذ تلقائياً عند تغيّر SelectedBook
        partial void OnSelectedBookChanged(BookDto? value)
        {
            // نعيد احتساب صلاحية تنفيذ الأمر
            DeleteBookCommand.NotifyCanExecuteChanged();
        }
    }
}
