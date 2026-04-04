//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using CommunityToolkit.Mvvm.Messaging;
//using CommunityToolkit.Mvvm.Messaging.Messages;
//using LibraryApp.BusinessLogic.Interfaces;
//using static LibraryApp.BusinessLogic.Models.DTOs;

//namespace LibraryApp.ViewModels.ViewModels
//{
//    public partial class LoginViewModel : ObservableObject
//    {
//        private readonly IUserService _userService;
//        private readonly IUserSessionService _sessionService;

//        [ObservableProperty]
//        private string username;

//        private string password;
//        public string Password
//        {
//            get => password;
//            set => SetProperty(ref password, value);
//        }

//        public bool IsAuthenticated { get; private set; } = false;

//        public LoginViewModel(IUserService userService, IUserSessionService sessionService)
//        {
//            _userService = userService;
//            _sessionService = sessionService;
//        }

//        [RelayCommand]
//        private async Task LoginAsync()
//        {
//            var UserDto = await _userService.AuthenticateAsync(Username, Password);
//            if (UserDto != null)
//            {
//                _sessionService.CurrentUser = UserDto;
//                IsAuthenticated = true;

//                WeakReferenceMessenger.Default.Send(new LoginSuccessMessage(UserDto));
//            }
//            else
//            {
//                IsAuthenticated = false;
//            }
//        }
//    }


//    // تعديل الرسالة لتستخدم UserDto بدلاً من User
//    public class LoginSuccessMessage : ValueChangedMessage<UserDto>
//    {
//        public LoginSuccessMessage(UserDto userDto) : base(userDto) { }
//    }
//}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using LibraryApp.BusinessLogic.Interfaces;
using static LibraryApp.BusinessLogic.Models.DTOs;

namespace LibraryApp.ViewModels.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _sessionService;

        [ObservableProperty]
        private string username;

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        [ObservableProperty]
        private string errorMessage;   // ← خاصية رسالة الخطأ

        public bool IsAuthenticated { get; private set; } = false;

        public LoginViewModel(IUserService userService, IUserSessionService sessionService)
        {
            _userService = userService;
            _sessionService = sessionService;
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            ErrorMessage = string.Empty;             // ← مسح الرسالة القديمة
            var userDto = await _userService.AuthenticateAsync(Username, Password);
            if (userDto != null)
            {
                _sessionService.CurrentUser = userDto;
                IsAuthenticated = true;
                WeakReferenceMessenger.Default.Send(new LoginSuccessMessage(userDto));
            }
            else
            {
                IsAuthenticated = false;
                ErrorMessage = "اسم المستخدم أو كلمة المرور غير صحيحة.";  // ← تعبئة الرسالة
            }
        }
    }

    public class LoginSuccessMessage : ValueChangedMessage<UserDto>
    {
        public LoginSuccessMessage(UserDto userDto) : base(userDto) { }
    }
}
