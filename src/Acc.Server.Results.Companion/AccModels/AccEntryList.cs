using System;
using System.Collections.Generic;

namespace Acc.Server.Results.Companion.AccModels;

public class AccEntryList
{
    public int ConfigVersion { get; set; }
    public List<AccEntryListEntry> Entries { get; set; }
    public bool ForceEntryList { get; set; }
}