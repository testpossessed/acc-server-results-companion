using System;
using System.Collections.Generic;

namespace Acc.Server.Results.Companion.AccModels;

public class AccLeaderBoardLine
{
    public AccCar Car { get; set; }
    public AccDriver CurrentDriver { get; set; }
    public int CurrentDriverIndex { get; set; }
    public AccTiming Timing { get; set; }
    public int MissingMandatoryPitstop { get; set; }
    public List<double> DriverTotalTimes { get; set; }
}