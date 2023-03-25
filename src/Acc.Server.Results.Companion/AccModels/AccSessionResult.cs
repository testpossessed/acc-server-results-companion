using System;
using System.Collections.Generic;

namespace Acc.Server.Results.Companion.AccModels;

internal class AccSessionResult
{
    public long BestLap { get; set; }
    public long[] BestSplits { get; set; }
    public bool IsWetSession { get; set; }
    public List<AccLeaderBoardLine> LeaderBoardLines { get; set; }
    public int Type { get; set; }
}