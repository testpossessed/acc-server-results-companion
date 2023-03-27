using System;
using System.Globalization;
using System.Windows.Data;

namespace Acc.Server.Results.Companion.Core.Converters;

internal class MissingMandatoryPitStopConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var intValue = (int)value;

        return intValue > 0? "Yes": "No";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}