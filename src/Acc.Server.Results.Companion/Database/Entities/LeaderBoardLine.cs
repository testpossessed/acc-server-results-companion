using System;

namespace Acc.Server.Results.Companion.Database.Entities;

public class LeaderBoardLine
{
    public int Id { get; set; }
    public string AverageLapTime { get; set; }
    public double AverageLapTimeMs { get; set; }
    public string BestLapTime { get; set; }
    public long BestLapTimeMs { get; set; }
    public string BestSector1Time { get; set; }
    public long BestSector1TimeMs { get; set; }
    public string BestSector2Time { get; set; }
    public long BestSector2TimeMs { get; set; }
    public string BestSector3Time { get; set; }
    public long BestSector3TimeMs { get; set; }
    public string CarClass { get; set; }
    public string CarName { get; set; }
    public string DriverName { get; set; }
    public string DriverShortName { get; set; }
    public int MissingMandatoryPitStop { get; set; }
    public int Position { get; set; }
    public int SessionId { get; set; }
    public string TeamName { get; set; }
}