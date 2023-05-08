using System;
using Acc.Server.Results.Companion.Core.Converters;
using Newtonsoft.Json;

namespace Acc.Server.Results.Companion.SimGrid.Models;

public class SimGridStandingsRace
{
    [JsonConverter(typeof(SingleOrArrayJsonConverter<bool?>))]
    public bool?[] Dnf { get; set; }

    [JsonConverter(typeof(SingleOrArrayJsonConverter<bool?>))]
    public bool?[] Dns { get; set; }

    [JsonConverter(typeof(SingleOrArrayJsonConverter<int?>))]
    public int?[] PenaltyPoints { get; set; }

    [JsonConverter(typeof(SingleOrArrayJsonConverter<double?>))]
    public double?[] PointsGiven { get; set; }
    
    [JsonConverter(typeof(SingleOrArrayJsonConverter<double?>))]
    public double?[] PointsTotal { get; set; }

    [JsonConverter(typeof(SingleOrArrayJsonConverter<int?>))]
    public int?[] Position { get; set; }
}