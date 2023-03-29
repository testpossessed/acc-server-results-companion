using System;

namespace Acc.Server.Results.Companion.Database.Entities;

public class Driver
{
    public string Id { get; set; }
    public string DriverCategory { get; set; }
    public int DriverCategoryCode { get; set; }
    public string FirstName { get; set; }
    public string FirstNameOverride { get; set; }
    public bool IsImported { get; set; }
    public string LastName { get; set; }
    public string LastNameOverride { get; set; }
    public string LastUpdateFilePath { get; set; }
    public string Nationality { get; set; }
    public int NationalityCode { get; set; }
    public int? NationalityCodeOverride { get; set; }
    public string NationalityOverride { get; set; }
    public string OurCategory { get; set; }
    public string ShortName { get; set; }
}