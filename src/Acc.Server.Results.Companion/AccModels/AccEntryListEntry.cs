using System;
using System.Collections.Generic;

namespace Acc.Server.Results.Companion.AccModels;

public class AccEntryListEntry
{
    public int ConfigVersion { get; set; }
    public string CustomCar { get; set; }
    public int DefaultGridPosition { get; set; }
    public List<AccEntryListDriver> Drivers { get; set; }
    public bool ForcedCarModel { get; set; }
    public bool IsServerAdmin { get; set; }
    public bool OverrideCarModelForCustomCar { get; set; }
    public bool OverrideDriverInfo { get; set; }
    public int RaceNumber { get; set; }
}