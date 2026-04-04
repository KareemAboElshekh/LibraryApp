using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using LibraryApp.BusinessLogic.Interfaces;
using LibraryApp.BusinessLogic.Models;

namespace LibraryApp.ViewModels.ViewModels
{
    public partial class WelcomeViewModel : ObservableObject
    {
        private readonly IUserSessionService _session;

        public string Greeting => $"Hallo {_session.CurrentUser?.Username}";
        public string CurrentUserTypeName => _session.CurrentUser?.TypeName ?? "";

        // أو نرسل رسائل من هنا:
        public IRelayCommand ShowChangePasswordCommand { get; }
        public IRelayCommand ShowBookReservationCommand { get; }
        public IRelayCommand ShowUserManagementCommand { get; }
        public IRelayCommand ShowBookManagementCommand { get; }

        public WelcomeViewModel(IUserSessionService session)
        {
            _session = session;

            ShowChangePasswordCommand = new RelayCommand(() =>
                WeakReferenceMessenger.Default.Send(new NavigationMessage(NavTarget.ChangePassword))
            );
            ShowBookReservationCommand = new RelayCommand(() =>
                WeakReferenceMessenger.Default.Send(new NavigationMessage(NavTarget.BookReservation))
            );
            ShowUserManagementCommand = new RelayCommand(() =>
                WeakReferenceMessenger.Default.Send(new NavigationMessage(NavTarget.UserManagement))
            );
            ShowBookManagementCommand = new RelayCommand(() =>
                WeakReferenceMessenger.Default.Send(new NavigationMessage(NavTarget.BookManagement))
            );
        }
    }

    // رسالة التنقل:
    public enum NavTarget { Welcome, ChangePassword, BookReservation, UserManagement, BookManagement }
    public class NavigationMessage : ValueChangedMessage<NavTarget>
    {
        public NavigationMessage(NavTarget target) : base(target) { }
    }
}
