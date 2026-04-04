using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryApp.BusinessLogic.Interfaces;
using LibraryApp.BusinessLogic.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static LibraryApp.BusinessLogic.Models.DTOs;

namespace LibraryApp.ViewModels.ViewModels
{
    public partial class UserManagementViewModel : ObservableObject
    {
        private readonly IUserService _userService;

        public ObservableCollection<UserDto> Users { get; } = new();

        [ObservableProperty] private UserDto? selectedUser;
        [ObservableProperty] private string message = "";

        // حقول الإدخال للإضافة
        [ObservableProperty] private string newUsername = "";
        [ObservableProperty] private string newPassword = "";
        [ObservableProperty] private string newPhone = "";
        [ObservableProperty] private int newTypeId;

        public IAsyncRelayCommand LoadUsersCommand { get; }
        public IAsyncRelayCommand AddUserCommand { get; }
        public IAsyncRelayCommand DeleteUserCommand { get; }

        public UserManagementViewModel(IUserService userService)
        {
            _userService = userService;

            LoadUsersCommand = new AsyncRelayCommand(LoadUsersAsync);
            AddUserCommand = new AsyncRelayCommand(AddUserAsync, CanAddUser);
            DeleteUserCommand = new AsyncRelayCommand(DeleteUserAsync, () => SelectedUser != null);

            _ = LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            Users.Clear();
            var list = await _userService.GetAllUsersAsync();
            foreach (var u in list) Users.Add(u);

            // بعد التحميل، حدّث صلاحيات الأوامر
            AddUserCommand.NotifyCanExecuteChanged();
            DeleteUserCommand.NotifyCanExecuteChanged();
        }

        private bool CanAddUser() =>
            !string.IsNullOrWhiteSpace(NewUsername) &&
            !string.IsNullOrWhiteSpace(NewPassword) &&
            !string.IsNullOrWhiteSpace(NewPhone) &&
            NewTypeId > 0;

        private async Task AddUserAsync()
        {
            var result = await _userService.CreateUserAsync(
                NewUsername, NewPassword, NewPhone, NewTypeId
            );

            Message = result.Success ? "تمت الإضافة بنجاح" : $"خطأ: {result.Message}";

            if (result.Success)
            {
                await LoadUsersAsync();
                // إعادة تهيئة الحقول
                NewUsername = "";
                NewPassword = "";
                NewPhone = "";
                NewTypeId = 0;
            }
        }

        private async Task DeleteUserAsync()
        {
            if (SelectedUser == null) return;
            var result = await _userService.DeleteUserAsync(SelectedUser.UserId);
            Message = result.Success ? "تم الحذف" : $"خطأ: {result.Message}";
            await LoadUsersAsync();
        }

        // هذه partial methods تُنفّذ تلقائياً عند تغيّر الخاصيّة
        partial void OnSelectedUserChanged(UserDto? value)
            => DeleteUserCommand.NotifyCanExecuteChanged();

        partial void OnNewUsernameChanged(string value)
            => AddUserCommand.NotifyCanExecuteChanged();
        partial void OnNewPasswordChanged(string value)
            => AddUserCommand.NotifyCanExecuteChanged();
        partial void OnNewPhoneChanged(string value)
            => AddUserCommand.NotifyCanExecuteChanged();
        partial void OnNewTypeIdChanged(int value)
            => AddUserCommand.NotifyCanExecuteChanged();
    }
}
