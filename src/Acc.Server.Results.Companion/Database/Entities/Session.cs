using System;
using System.Collections.Generic;
using Acc.Server.Results.Companion.Core;

namespace Acc.Server.Results.Companion.Database.Entities;

public class Session
{
    public int Id { get; set; }

    public string BestLap => this.BestLapMs.ToTimingString();
    public string BestSector1 => this.BestSector1Ms.ToTimingString();
    public string BestSector2 => this.BestSector2Ms.ToTimingString();
    public string BestSector3 => this.BestSector3Ms.ToTimingString();
    public string DisplayName => $"{this.TrackName} {this.SessionType} {this.TimeStamp}";
    public long BestLapMs { get; set; }
    public long BestSector1Ms { get; set; }
    public long BestSector2Ms { get; set; }
    public long BestSector3Ms { get; set; }
    public string FilePath { get; set; }
    public bool IsWetSession { get; set; }
    public string MetaData { get; set; }
    public int RaceWeekendIndex { get; set; }
    public int ServerId { get; set; }
    public string ServerName { get; set; }
    public int SessionIndex { get; set; }
    public string SessionType { get; set; }
    public DateTime TimeStamp { get; set; }
    public string TrackName { get; set; }

    public ICollection<LeaderBoardLine> LeaderBoardLines { get; set; }
}