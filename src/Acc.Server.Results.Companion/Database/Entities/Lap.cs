using System;

namespace Acc.Server.Results.Companion.Database.Entities;

public class Lap
{
    public int Id { get; set; }
    public string Car { get; set; }
    public string Driver { get; set; }
    public string DriverCategory { get; set; }
    public bool IsValid { get; set; }
    public string LapTime { get; set; }
    public long LapTimeMs { get; set; }
    public string Nationality { get; set; }
    public int NationalityCode { get; set; }
    public string OurCategory { get; set; }
    public string Sector1Time { get; set; }
    public long Sector1TimeMs { get; set; }
    public string Sector2Time { get; set; }
    public long Sector2TimeMs { get; set; }
    public string Sector3Time { get; set; }
    public long Sector3TimeMs { get; set; }
    public int SessionId { get; set; }
}