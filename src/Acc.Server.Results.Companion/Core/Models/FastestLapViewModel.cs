using System;

namespace Acc.Server.Results.Companion.Core.Models;

public class FastestLapViewModel
{
    public string Car { get; set; }
    public string Driver { get; set; }
    public string LapTime { get; set; }
    public long LapTimeMs { get; set; }
    public string Sector1Time { get; set; }
    public string Sector2Time { get; set; }
    public string Sector3Time { get; set; }
    public string Track { get; set; }
}