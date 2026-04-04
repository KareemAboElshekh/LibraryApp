using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryApp.BusinessLogic.Interfaces;

namespace LibraryApp.ViewModels.ViewModels
{
    public partial class ChangePasswordViewModel : ObservableObject
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _session;

        [ObservableProperty] private string oldPassword = "";
        [ObservableProperty] private string newPassword = "";
        [ObservableProperty] private string message = "";

        public IAsyncRelayCommand ChangePasswordCommand { get; }

        public ChangePasswordViewModel(IUserService userService, IUserSessionService session)
        {
            _userService = userService;
            _session = session;
            ChangePasswordCommand = new AsyncRelayCommand(ChangePasswordAsync, CanChangePassword);
        }

        private bool CanChangePassword() =>
            !string.IsNullOrWhiteSpace(OldPassword) &&
            !string.IsNullOrWhiteSpace(NewPassword) &&
            _session.CurrentUser != null;

        private async Task ChangePasswordAsync()
        {
            var user = _session.CurrentUser!;
            var result = await _userService.ChangePasswordAsync(user.UserId, OldPassword, NewPassword);
            Message = result.Success
                      ? "تم تغيير كلمة المرور بنجاح."
                      : $"فشل: {result.Message}";
        }
    }
}
