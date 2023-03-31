using System;
using System.Globalization;
using System.Windows.Data;

namespace Acc.Server.Results.Companion.Core.Converters;

public class NationalityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var valueAsString = (string)value;
        return valueAsString switch
        {
            "Any" => null,
            _ => valueAsString
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}