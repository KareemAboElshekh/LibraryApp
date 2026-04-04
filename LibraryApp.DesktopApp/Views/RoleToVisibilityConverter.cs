using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LibraryApp.DesktopApp.Views
{
    public class RoleToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var role = value as string;
            var mustBe = parameter as string;
            return string.Equals(role, mustBe, StringComparison.OrdinalIgnoreCase)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
