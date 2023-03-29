using System;

namespace Acc.Server.Results.Companion.Database.Entities;

public class Penalty
{
    public int Id { get; set; }
    public string Car { get; set; }
    public int ClearedOnLap { get; set; }
    public string Driver { get; set; }
    public string DriverCategory { get; set; }
    public bool IsPostRacePenalty { get; set; }
    public string Nationality { get; set; }
    public int NationalityCode { get; set; }
    public string OurCategory { get; set; }
    public string PenaltyCode { get; set; }
    public int PenaltyValue { get; set; }
    public string Reason { get; set; }
    public int SessionId { get; set; }
    public int ViolationOnLap { get; set; }
}