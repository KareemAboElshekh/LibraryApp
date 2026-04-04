using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using LibraryApp.BusinessLogic.Models;
using LibraryApp.ViewModels.ViewModels;
using static LibraryApp.BusinessLogic.Models.DTOs;
using LibraryApp.DesktopApp.Views;

namespace LibraryApp.DesktopApp
{
    public partial class LoginWindow : Window
    {
        public LoginWindow(LoginViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            // مزامنة الباسورد
            PasswordBox.PasswordChanged += (s, e) =>
            {
                if (DataContext is LoginViewModel vm)
                    vm.Password = PasswordBox.Password;
            };

            // عند نجاح الدخول، افتح WelcomeWindow
            WeakReferenceMessenger.Default.Register<LoginSuccessMessage>(this, (r, m) =>
            {
                var welcome = App.Host.Services.GetRequiredService<WelcomeWindow>();
                welcome.Show();
                this.Close();
            });
        }
    }

}

