using System;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using LibraryApp.BusinessLogic.Models;
using static LibraryApp.BusinessLogic.Models.DTOs;
using static System.Net.Mime.MediaTypeNames;
using CommunityToolkit.Mvvm.Messaging;

namespace LibraryApp.ViewModels.ViewModels
{
    public partial class ShellViewModel : ObservableObject
    {
        // خاصية لتخزين المستخدم الحالي
        private UserDto? _currentUser;
        public UserDto? CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        // رسالة الترحيب
        public string Greeting => CurrentUser != null ? $"Hallo {CurrentUser.Username}" : string.Empty;

        // الأوامر للتنقل بين الشاشات
        public IRelayCommand ShowChangePasswordCommand { get; }
        public IRelayCommand ShowBookReservationCommand { get; }
        public IRelayCommand ShowUserManagementCommand { get; }
        public IRelayCommand ShowBookManagementCommand { get; }
        public IRelayCommand BackToWelcomeCommand { get; }

        private readonly ChangePasswordViewModel _changePasswordViewModel;
        private readonly BookReservationViewModel _bookReservationViewModel;
        private readonly UserManagementViewModel _userManagementViewModel;
        private readonly BookManagementViewModel _bookManagementViewModel;

        private object? _currentPageViewModel;
        public object? CurrentPageViewModel
        {
            get => _currentPageViewModel;
            set => SetProperty(ref _currentPageViewModel, value);
        }

        public ShellViewModel(
            ChangePasswordViewModel changePasswordViewModel,
            BookReservationViewModel bookReservationViewModel,
            UserManagementViewModel userManagementViewModel,
            BookManagementViewModel bookManagementViewModel)
        {
            _changePasswordViewModel = changePasswordViewModel;
            _bookReservationViewModel = bookReservationViewModel;
            _userManagementViewModel = userManagementViewModel;
            _bookManagementViewModel = bookManagementViewModel;

            // تهيئة الأوامر
            ShowChangePasswordCommand = new RelayCommand(ExecuteShowChangePassword);
            ShowBookReservationCommand = new RelayCommand(ExecuteShowBookReservation);
            ShowUserManagementCommand = new RelayCommand(ExecuteShowUserManagement);
            ShowBookManagementCommand = new RelayCommand(ExecuteShowBookManagement);
            // داخل ShellViewModel
            BackToWelcomeCommand = new RelayCommand(() =>
                WeakReferenceMessenger.Default.Send(new NavigationMessage(NavTarget.Welcome))
            );

            // افتراضيًا عرض صفحة الحجز/الإرجاع بعد تسجيل الدخول
            CurrentPageViewModel = _bookReservationViewModel;
        }

        private void ExecuteShowChangePassword() => CurrentPageViewModel = _changePasswordViewModel;
        private void ExecuteShowBookReservation() => CurrentPageViewModel = _bookReservationViewModel;
        private void ExecuteShowUserManagement() => CurrentPageViewModel = _userManagementViewModel;
        private void ExecuteShowBookManagement() => CurrentPageViewModel = _bookManagementViewModel;

      

        // …

        // وظائف مساعدة للتنقل عبر الرسائل
        public void ShowChangePassword() => ShowChangePasswordCommand.Execute(null);
        public void ShowBookReservation() => ShowBookReservationCommand.Execute(null);
        public void ShowUserManagement() => ShowUserManagementCommand.Execute(null);
        public void ShowBookManagement() => ShowBookManagementCommand.Execute(null);
    }
}
