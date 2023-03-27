using System;
using System.Collections.Generic;
using Acc.Server.Results.Companion.Core;

namespace Acc.Server.Results.Companion.AccModels;

public class AccLap
{
    public string Sector1Time =>
        this.Splits[0]
            .ValidatedValue()
            .ToTimingString();
    public string Sector2Time =>
        this.Splits[1]
            .ValidatedValue()
            .ToTimingString();
    public string Sector3Time =>
        this.Splits[2]
            .ValidatedValue()
            .ToTimingString();
    public string Timestamp => this.TimestampMS.ToTimingString();
    public int CarId { get; set; }
    public int DriverId { get; set; }
    public int Flags { get; set; }
    public double Fuel { get; set; }
    public bool IsValidForBest { get; set; }
    public long LapTime { get; set; }
    public List<long> Splits { get; set; }
    public double TimestampMS { get; set; }

    public string GetLapTime()
    {
        return this.LapTime.ValidatedValue()
                   .ToTimingString();
    }
}