using System;

namespace Acc.Server.Results.Companion.Core.Models;

public class DriverPerformanceItem
{
    public double Consistency { get; set; } = 999;
    public string ConsistencyDisplay { get; set; } = "Not Rated";
    public string DriverName { get; set; }
    public int InvalidLapCount { get; set; }
    public int PenaltyCount { get; set; }
    public int PenaltyValueTotal { get; set; }
    public int SessionCount { get; set; }
    public int ValidLapCount { get; set; }
    public double ValidRatio { get; set; }
    public string ValidRatioDisplay { get; set; } = "Not Rated";
}