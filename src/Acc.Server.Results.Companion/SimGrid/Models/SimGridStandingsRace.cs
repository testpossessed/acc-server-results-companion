using System;

namespace Acc.Server.Results.Companion.SimGrid.Models;

public class SimGridStandingsRace
{
    public bool? Dnf { get; set; }
    public bool? Dns { get; set; }
    public int? PenaltyPoints { get; set; }
    public double? PointsGiven { get; set; }
    public double? PointsTotal { get; set; }
    public int? Position { get; set; }
}