using System;
using Acc.Server.Results.Companion.Database.Entities;

namespace Acc.Server.Results.Companion.Core.Models;

public class LapViewModel
{
    public LapViewModel(Lap lap, ServerDetails serverDetails)
    {
        this.Car = lap.Car;
        this.DriverCategory = lap.DriverCategory;
        this.Driver = lap.Driver;
        this.DriverClass = this.GetDriverClass(lap, serverDetails);
        this.IsValid = lap.IsValid;
        this.LapTime = lap.LapTime;
        this.LapTimeMs = lap.LapTimeMs;
        this.Nationality = lap.Nationality;
        this.NationalityCode = lap.NationalityCode;
        this.Sector1Time = lap.Sector1Time;
        this.Sector1TimeMs = this.Sector1TimeMs;
        this.Sector2Time = lap.Sector2Time;
        this.Sector2TimeMs = this.Sector2TimeMs;
        this.Sector3Time = lap.Sector3Time;
        this.Sector3TimeMs = this.Sector3TimeMs;
    }

    public string Car { get; set; }
    public string Driver { get; set; }
    public string DriverCategory { get; set; }
    public string DriverClass { get; set; }
    public bool IsValid { get; set; }
    public string LapTime { get; set; }
    public long LapTimeMs { get; set; }
    public string Nationality { get; set; }
    public int NationalityCode { get; set; }
    public string Sector1Time { get; set; }
    public long Sector1TimeMs { get; set; }
    public string Sector2Time { get; set; }
    public long Sector2TimeMs { get; set; }
    public string Sector3Time { get; set; }
    public long Sector3TimeMs { get; set; }

    private string GetDriverClass(Lap lap, ServerDetails serverDetails)
    {
        return lap.DriverCategory switch
        {
            "Silver" => serverDetails.SilverClassification,
            "Gold" => serverDetails.GoldClassification,
            "Platinum" => serverDetails.PlatinumClassification,
            _ => serverDetails.BronzeClassification
        };
    }
}