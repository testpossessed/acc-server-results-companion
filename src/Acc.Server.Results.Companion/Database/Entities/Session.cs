using System;

namespace Acc.Server.Results.Companion.Database.Entities;

public class Session
{
    public int Id { get; set; }
    public string FilePath { get; set; }
    public string MetaData { get; set; }
    public int RaceWeekendIndex { get; set; }
    public int ServerId { get; set; }
    public string ServerName { get; set; }
    public int SessionIndex { get; set; }
    public string SessionType { get; set; }
    public DateTime TimeStamp { get; set; }
    public string TrackName { get; set; }

    public string DisplayName => $"{this.TrackName} {this.SessionType} {this.TimeStamp}";
}