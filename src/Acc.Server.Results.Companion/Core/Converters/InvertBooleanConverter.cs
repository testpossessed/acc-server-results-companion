using System;
using System.Globalization;
using System.Windows.Data;

namespace Acc.Server.Results.Companion.Core.Converters;

public class InvertBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var boolValue = (bool)value;
        return !boolValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var boolValue = (bool)value;
        return !boolValue;
    }
}