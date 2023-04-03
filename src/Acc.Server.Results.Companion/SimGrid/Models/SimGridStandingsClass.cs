using System;
using System.Collections.Generic;

namespace Acc.Server.Results.Companion.SimGrid.Models;

public class SimGridStandingsClass
{
    public string CarClass { get; set; }
    public List<SimGridStanding> Standings { get; set; }
}