using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using LibraryApp.ViewModels.ViewModels;
using LibraryApp.DesktopApp; // لكي يتعرف على App.Host
using LibraryApp.DesktopApp.Views;


namespace LibraryApp.DesktopApp.Views
{
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow(WelcomeViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;

            // 1) سجل استقبال رسائل التنقل
            WeakReferenceMessenger.Default.Register<NavigationMessage>(this, (r, m) =>
            {
                var shell = App.Host.Services.GetRequiredService<ShellWindow>();
                if (shell.DataContext is ShellViewModel shellVm)
                {
                    switch (m.Value)
                    {
                        case NavTarget.ChangePassword:   shellVm.ShowChangePassword();   break;
                        case NavTarget.BookReservation:  shellVm.ShowBookReservation();  break;
                        case NavTarget.UserManagement:   shellVm.ShowUserManagement();   break;
                        case NavTarget.BookManagement:   shellVm.ShowBookManagement();   break;
                        case NavTarget.Welcome:          /* لا شيء */                   break;
                    }
                }
                shell.Show();
                this.Close();
            });

            // 2) فك التسجيل عند الإغلاق
            this.Closed += (s, e) =>
                WeakReferenceMessenger.Default.Unregister<NavigationMessage>(this);
        }
    }

}
