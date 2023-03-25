using System;
using System.Globalization;
using System.Windows.Data;

namespace Acc.Server.Results.Companion.Core.Converters;

public class SessionTypeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var valueAsString = (string)value;
        return valueAsString switch
        {
            "Q" => "Qualifying",
            "P" => "Practice",
            _ => "Race"
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var valueAsString = (string)value;
        return valueAsString switch
        {
            "Qualifying" => "Q",
            "Practice" => "P",
            _ => "R"
        };
    }
}