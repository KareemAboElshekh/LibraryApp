
using System.Windows;
using LibraryApp.ViewModels;
using LibraryApp.ViewModels.ViewModels;

namespace LibraryApp.DesktopApp
{
    public partial class MainWindow : Window
    {
        public MainWindow(ShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
