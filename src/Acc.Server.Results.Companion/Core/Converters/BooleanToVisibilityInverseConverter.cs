using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Acc.Server.Results.Companion.Core.Converters;

public class BooleanToVisibilityInverseConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is not bool boolValue)
        {
            throw new ArgumentException(
                "BooleanToVisibilityConverter requires a boolean value to convert");
        }

        return boolValue? Visibility.Collapsed: Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is not Visibility visibilityValue)
        {
            throw new ArgumentException(
                "BooleanToVisibilityConverter requires a Visibility value to convert back");
        }

        return visibilityValue == Visibility.Collapsed;
    }
}