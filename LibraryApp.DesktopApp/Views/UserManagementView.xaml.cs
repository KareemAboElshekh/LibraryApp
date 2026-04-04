using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using LibraryApp.ViewModels.ViewModels;

namespace LibraryApp.DesktopApp.Views
{
    public partial class UserManagementView : UserControl
    {
        public UserManagementView()
            : this(App.Host.Services.GetRequiredService<UserManagementViewModel>())
        { }

        public UserManagementView(UserManagementViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}