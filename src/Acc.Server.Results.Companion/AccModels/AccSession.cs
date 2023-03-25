using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Acc.Server.Results.Companion.AccModels;

internal class AccSession
{
    public List<AccLap> Laps { get; set; }
    public string MetaData { get; set; }
    public List<AccPenalty> Penalties { get; set; }
    [JsonProperty(PropertyName = "post_race_penalties")]
    public List<AccPenalty> PostRacePenalties { get; set; }
    public int RaceWeekendIndex { get; set; }
    public string ServerName { get; set; }
    public int SessionIndex { get; set; }
    public AccSessionResult SessionResult { get; set; }
    public string SessionType { get; set; }
    public string TrackName { get; set; }
}