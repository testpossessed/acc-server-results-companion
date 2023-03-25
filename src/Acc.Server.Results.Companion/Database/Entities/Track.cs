using System;

namespace Acc.Server.Results.Companion.Database.Entities;

public class Track
{
    public int Id { get; set; }
    public string AccTrackId { get; set; }
    public int Corners { get; set; }
    public string Name { get; set; }
    public double TrackLength { get; set; }
}