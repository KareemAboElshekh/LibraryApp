using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using LibraryApp.ViewModels.ViewModels;

namespace LibraryApp.DesktopApp.Views
{
    public partial class BookReservationView : UserControl
    {
        public BookReservationView()
            : this(App.Host.Services.GetRequiredService<BookReservationViewModel>())
        { }

        public BookReservationView(BookReservationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}