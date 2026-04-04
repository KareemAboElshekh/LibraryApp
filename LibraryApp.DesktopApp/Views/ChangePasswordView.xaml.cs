
using System.Windows.Controls;
using LibraryApp.ViewModels.ViewModels;

namespace LibraryApp.DesktopApp.Views
{
    public partial class ChangePasswordView : UserControl
    {
        public ChangePasswordView()
        {
            InitializeComponent();

            // عند تغيير DataContext، اربط PasswordBox بالأوامر
            this.DataContextChanged += (s, e) =>
            {
                if (e.NewValue is ChangePasswordViewModel vm)
                {
                    // مزامنة كلمة المرور القديمة
                    OldPasswordBox.PasswordChanged += (sender, args) =>
                    {
                        vm.OldPassword = OldPasswordBox.Password;
                        vm.ChangePasswordCommand.NotifyCanExecuteChanged();
                    };

                    // مزامنة كلمة المرور الجديدة
                    NewPasswordBox.PasswordChanged += (sender, args) =>
                    {
                        vm.NewPassword = NewPasswordBox.Password;
                        vm.ChangePasswordCommand.NotifyCanExecuteChanged();
                    };
                }
            };
        }
    }
}