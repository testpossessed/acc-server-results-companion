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

        internal static string ToThreeCharacterCode(this AccNationality nationality)
        {
            switch(nationality)
            {
                case AccNationality.Any:
                    return "ZZZ";
                case AccNationality.Germany:
                    return "DEU";
                case AccNationality.Spain:
                    return "ESP";
                case AccNationality.GreatBritain:
                    return "GBR";
                case AccNationality.Switzerland:
                    return "CHE";
                case AccNationality.Austria:
                    return "AUT";
                case AccNationality.Netherlands:
                    return "NLD";
                case AccNationality.Monaco:
                    return "MCO";
                case AccNationality.Ireland:
                    return "IRL";
                case AccNationality.SouthAfrica:
                    return "ZAF";
                case AccNationality.PuertoRico:
                    return "PRI";
                case AccNationality.Slovakia:
                    return "SVK";
                case AccNationality.Oman:
                    return "OMN";
                case AccNationality.Greece:
                    return "GRC";
                case AccNationality.SouthKorea:
                    return "KOR";
                case AccNationality.Lebanon:
                    return "LBN";
                case AccNationality.Denmark:
                    return "DNK";
                case AccNationality.Croatia:
                    return "HRV";
                case AccNationality.China:
                    return "CHN";
                case AccNationality.Portugal:
                    return "PRT";
                case AccNationality.Singapore:
                    return "SGP";
                case AccNationality.Indonesia:
                    return "IDN";
                case AccNationality.NewZealand:
                    return "NZL";
                case AccNationality.SanMarino:
                    return "SMR";
                case AccNationality.UAE:
                    return "ARE";
                case AccNationality.Kuwait:
                    return "KWT";
                case AccNationality.HongKong:
                    return "HKG";
                case AccNationality.Japan:
                    return "JPN";
                case AccNationality.Bulgaria:
                    return "BGR";
                case AccNationality.Latvia:
                    return "LVA";
                case AccNationality.Lithuania:
                    return "LTU";
                case AccNationality.Malaysia:
                    return "MYS";
                case AccNationality.Nepal:
                    return "NPL";
                case AccNationality.NewCaledonia:
                    return "NCL";
                case AccNationality.Nigeria:
                    return "NER";
                case AccNationality.NorthernIreland:
                    return "NIR";
                case AccNationality.PapuaNewGuinea:
                    return "PNG";
                case AccNationality.Philippines:
                    return "PHL";
                case AccNationality.Romania:
                    return "ROU";
                case AccNationality.Serbia:
                    return "SRB";
                case AccNationality.Slovenia:
                    return "SVK";
                case AccNationality.Taiwan:
                    return "TWN";
                case AccNationality.Iran:
                    return "IRN";
                case AccNationality.Bahrain:
                    return "BHR";
                case AccNationality.Zimbabwe:
                    return "ZWE";
                case AccNationality.ChineseTaipei:
                    return "CHN";
                case AccNationality.Uruguay:
                    return "URU";
                default:
                    return nationality.ToString()[..3].ToUpperInvariant();
            }
        }
    }
}