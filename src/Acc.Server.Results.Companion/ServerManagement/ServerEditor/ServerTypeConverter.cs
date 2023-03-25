using System;
using System.Globalization;
using System.Windows.Data;

namespace Acc.Server.Results.Companion.ServerManagement.ServerEditor
{
    internal class ServerTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)parameter == (string)value);
        }

        public object? ConvertBack(object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return (bool)value? parameter: null;
        }
    }
}