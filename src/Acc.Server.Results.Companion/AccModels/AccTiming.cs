using System;
using System.Collections.Generic;
using Acc.Server.Results.Companion.Core;

namespace Acc.Server.Results.Companion.AccModels;

public class AccTiming
{
    public double AverageLapTimeMs => (this.LapCount > 0? ((double)this.TotalTime.ValidatedValue()) / this.LapCount: 0);
	public string AverageLapTime => this.AverageLapTimeMs.ToTimingString();
	public string BestLapTime =>
		this.BestLap.ValidatedValue()
		    .ToTimingString();
	public string BestSector1 => (this.LapCount > 0? this.BestSplits[0]: 0).ToTimingString();
	public string BestSector2 => (this.LapCount > 0? this.BestSplits[1]: 0).ToTimingString();
	public string BestSector3 => (this.LapCount > 0? this.BestSplits[2]: 0).ToTimingString();
	public long BestLap { get; set; }
	public List<long> BestSplits { get; set; }
	public int LapCount { get; set; }
	public long LastLap { get; set; }
	public long LastSplitId { get; set; }
	public List<long> LastSplits { get; set; }
	public long TotalTime { get; set; }
}