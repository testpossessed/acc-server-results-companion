using System;
using System.Collections.Generic;

namespace Acc.Server.Results.Companion.AccModels;

public class AccCar
{
    public int CarId { get; set; }
    public int RaceNumber { get; set; }
    public int CarModel { get; set; }
    public int CupCategory { get; set; }
    public string CarGroup { get; set; }
    public string TeamName { get; set; }
    public int Nationality { get; set; }
    public long CarGuid { get; set; }
    public long TeamGuid { get; set; }
    public List<AccDriver> Drivers { get; set; }

    public AccDriver GetDriverByIndex(int index)
    {
        return this.Drivers[index];
    }
}