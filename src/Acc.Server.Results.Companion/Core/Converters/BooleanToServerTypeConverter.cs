using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Acc.Server.Results.Companion.Core.Converters;

public class BooleanToServerTypeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is not bool boolValue)
        {
            throw new ArgumentException(
                "BooleanToServerTypeConverter requires a boolean value to convert");
        }

        return boolValue? "Folder": "FTP";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is not string serverModeValue)
        {
            throw new ArgumentException(
                "BooleanToServerTypeConverter requires a string value to convert back");
        }

        return serverModeValue == "Folder";
    }
}