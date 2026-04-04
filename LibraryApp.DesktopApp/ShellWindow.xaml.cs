using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using LibraryApp.ViewModels.ViewModels;
using LibraryApp.DesktopApp.Views;

namespace LibraryApp.DesktopApp
{
    public partial class ShellWindow : Window
    {
        public ShellWindow(ShellViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void BackToWelcome_Click(object sender, RoutedEventArgs e)
        {
            // 1) اجلب نافذة الترحيب (Singleton أو Transient حسب تسجيلك)
            var welcome = App.Host.Services.GetRequiredService<WelcomeWindow>();
            // 2) أظهرها
            welcome.Show();
            // 3) أغلق هذه النافذة
            this.Close();
        }
    }
}
