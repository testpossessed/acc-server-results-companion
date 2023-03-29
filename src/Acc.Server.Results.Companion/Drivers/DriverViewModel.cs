using System;
using Acc.Server.Results.Companion.Database.Entities;

namespace Acc.Server.Results.Companion.Drivers;

public class DriverViewModel
{
    public DriverViewModel(Driver driver)
    {
        this.Driver = driver;
        this.Id = driver.Id;
        this.FirstName = string.IsNullOrWhiteSpace(driver.FirstNameOverride)
                             ? driver.FirstName
                             : driver.FirstNameOverride;
        this.LastName = string.IsNullOrWhiteSpace(driver.LastNameOverride)
                            ? driver.LastName
                            : driver.LastNameOverride;
        this.ShortName = driver.ShortName;
        this.Nationality = driver.NationalityCodeOverride.HasValue
                               ? driver.NationalityOverride
                               : driver.Nationality;
        this.DriverCategory = driver.DriverCategory;
        this.OurCategory = driver.OurCategory;
        this.IsImported = driver.IsImported? "Yes": "No";
    }

    public string Id { get; }
    public Driver Driver { get; }
    public string DriverCategory { get; }
    public string FirstName { get; }
    public string IsImported { get; }
    public string LastName { get; }
    public string Nationality { get; }
    public string OurCategory { get; }
    public string ShortName { get; }
}