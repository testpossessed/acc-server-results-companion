using System;

namespace Acc.Server.Results.Companion.AccModels;

internal class AccPenalty
{
    public int CarId { get; set; }
    public int DriverIndex { get; set; }
    public string Reason { get; set; }
    public string Penalty { get; set; }
    public int PenaltyValue { get; set; }
    public int ViolationOnLap { get; set; }
    public int ClearedOnLap { get; set; }
}