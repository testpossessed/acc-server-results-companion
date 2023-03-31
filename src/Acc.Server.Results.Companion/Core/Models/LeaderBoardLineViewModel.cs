using System;
using Acc.Server.Results.Companion.Database.Entities;

namespace Acc.Server.Results.Companion.Core.Models;

public class LeaderBoardLineViewModel
{
    public LeaderBoardLineViewModel(LeaderBoardLine leaderBoardLine, ServerDetails serverDetails)
    {
        this.AverageLapTime = leaderBoardLine.AverageLapTime;
        this.AverageLapTimeMs = leaderBoardLine.AverageLapTimeMs;
        this.BestLapTime = leaderBoardLine.BestLapTime;
        this.BestLapTimeMs = leaderBoardLine.BestLapTimeMs;
        this.BestSector1Time = leaderBoardLine.BestSector1Time;
        this.BestSector1TimeMs = leaderBoardLine.BestSector1TimeMs;
        this.BestSector2Time = leaderBoardLine.BestSector2Time;
        this.BestSector2TimeMs = leaderBoardLine.BestSector2TimeMs;
        this.BestSector3Time = leaderBoardLine.BestSector3Time;
        this.BestSector3TimeMs = leaderBoardLine.BestSector3TimeMs;
        this.CarClass = leaderBoardLine.CarClass;
        this.CarName = leaderBoardLine.CarName;
        this.DriverCategory = leaderBoardLine.DriverCategory;
        this.DriverClass = this.GetDriverClass(leaderBoardLine, serverDetails);
        this.DriverName = leaderBoardLine.DriverName;
        this.DriverShortName = leaderBoardLine.DriverShortName;
        this.MissingMandatoryPitStop = leaderBoardLine.MissingMandatoryPitStop;
        this.Nationality = leaderBoardLine.Nationality;
        this.NationalityCode = leaderBoardLine.NationalityCode;
        this.Position = leaderBoardLine.Position;
        this.TeamName = leaderBoardLine.TeamName;
    }

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
    public string DriverCategory { get; set; }
    public string DriverClass { get; set; }
    public string DriverName { get; set; }
    public string DriverShortName { get; set; }
    public int MissingMandatoryPitStop { get; set; }
    public string Nationality { get; set; }
    public int NationalityCode { get; set; }
    public int Position { get; set; }
    public string TeamName { get; set; }

    private string GetDriverClass(LeaderBoardLine leaderBoardLine, ServerDetails serverDetails)
    {
        return leaderBoardLine.DriverCategory switch
        {
            "Silver" => serverDetails.SilverClassification,
            "Gold" => serverDetails.GoldClassification,
            "Platinum" => serverDetails.PlatinumClassification,
            _ => serverDetails.BronzeClassification
        };
    }
}