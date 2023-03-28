using System;
using Acc.Server.Results.Companion.AccEnums;

namespace Acc.Server.Results.Companion.Core
{
    internal static class Extensions
    {
        internal static string ToTimeString(this double timeMs)
        {
            return TimeSpan.FromMilliseconds(timeMs)
                           .ToString(Constants.TimeFormat);
        }

        internal static string ToTimeString(this int timeMs)
        {
            return TimeSpan.FromMilliseconds(timeMs)
                           .ToString(Constants.TimeFormat);
        }

        internal static string ToTimeString(this long timeMs)
        {
            return TimeSpan.FromMilliseconds(timeMs)
                           .ToString(Constants.TimeFormat);
        }

        internal static string ToTimingString(this double timingMs)
        {
            return TimeSpan.FromMilliseconds(timingMs)
                           .ToString(Constants.TimingFormat);
        }

        internal static string ToTimingString(this float timingMs)
        {
            return TimeSpan.FromMilliseconds(timingMs)
                           .ToString(Constants.TimingFormat);
        }

        internal static string ToTimingString(this int timingMs)
        {
            return TimeSpan.FromMilliseconds(timingMs)
                           .ToString(Constants.TimingFormat);
        }

        internal static string ToTimingString(this long timingMs)
        {
            return TimeSpan.FromMilliseconds(timingMs)
                           .ToString(Constants.TimingFormat);
        }

        internal static string ToFriendlyName(this AccNationality nationality)
        {
            return nationality switch
                   {
                       AccNationality.ChineseTaipei => "Chinese Taipei",
                       AccNationality.CzechRepublic => "Czech Republic",
                       AccNationality.GreatBritain => "Great Britain",
                       AccNationality.HongKong => "Hong Kong",
                       AccNationality.NewCaledonia => "New Caledonia",
                       AccNationality.NewZealand => "New Zealand",
                       AccNationality.NorthernIreland => "Northern Ireland",
                       AccNationality.PapuaNewGuinea => "Papua New Guinea",
                       AccNationality.PuertoRico => "Puerto Rico",
                       AccNationality.SanMarino => "San Marino",
                       AccNationality.SaudiArabia => "Saudi Arabia",
                       AccNationality.SouthAfrica => "South Africa",
                       AccNationality.SouthKorea => "South Korea",
                       _ => nationality.ToString()
                   };
        }

        internal static long ValidatedValue(this long longValue)
        {
            return longValue >= int.MaxValue? 0: longValue;
        }

        internal static double ValidatedValue(this double doubleValue)
        {
            return doubleValue >= int.MaxValue? 0: doubleValue;
        }
    }
}