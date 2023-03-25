using System;

namespace Acc.Server.Results.Companion.Database.Entities;

public class Car
{
    public int Id { get; set; }
    public string Class { get; set; }
    public string AccModelName { get; set; }
    public int MaxFuel { get; set; }
    public int MaxRpm { get; set; }
    public int AccModelId { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
}