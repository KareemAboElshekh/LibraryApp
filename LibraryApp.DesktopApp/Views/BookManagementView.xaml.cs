using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using LibraryApp.ViewModels.ViewModels;

namespace LibraryApp.DesktopApp.Views
{
    public partial class BookManagementView : UserControl
    {
        public BookManagementView()
            : this(App.Host.Services.GetRequiredService<BookManagementViewModel>())
        { }

        public BookManagementView(BookManagementViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}