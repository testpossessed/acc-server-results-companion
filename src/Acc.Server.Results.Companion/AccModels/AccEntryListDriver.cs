using System;
using Acc.Server.Results.Companion.AccEnums;

namespace Acc.Server.Results.Companion.AccModels;

public class AccEntryListDriver
{
    public AccDriverCategory DriverCategory { get; set; }
    public string FirstName { get; set; }
    public bool IsSystemAdmin { get; set; }
    public string LastName { get; set; }
    public AccNationality Nationality { get; set; }
    public string PlayerId { get; set; }
    public string ShortName { get; set; }
}