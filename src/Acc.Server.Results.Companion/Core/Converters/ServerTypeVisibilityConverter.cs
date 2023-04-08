using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Acc.Server.Results.Companion.Core.Converters
{
    internal class ServerTypeVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)parameter == (string)value)? Visibility.Visible: Visibility.Hidden;
            ;
        }

        public object ConvertBack(object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}