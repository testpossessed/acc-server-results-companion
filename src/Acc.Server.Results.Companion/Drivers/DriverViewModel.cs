using System;
using Acc.Server.Results.Companion.AccEnums;
using Acc.Server.Results.Companion.Database.Entities;

namespace Acc.Server.Results.Companion.Drivers;

public class DriverViewModel
{
    public DriverViewModel(Driver driver, ServerDetails serverDetails)
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
        this.DriverCategory = ((AccDriverCategory)driver.DriverCategoryCode).ToString();
        this.DriverCategoryCode = driver.DriverCategoryCode;
        this.DriverClass = this.GetDriverClass(driver, serverDetails);
        this.IsImported = driver.IsImported? "Yes": "No";
    }

    public string Id { get; }
    public Driver Driver { get; }
    public int DriverCategoryCode { get; }
    public string DriverCategory { get; }
    public string DriverClass { get; }
    public string FirstName { get; }
    public string IsImported { get; }
    public string LastName { get; }
    public string Nationality { get; }
    public string ShortName { get; }

    private string GetDriverClass(Driver driver, ServerDetails serverDetails)
    {
        return (AccDriverCategory)driver.DriverCategoryCode switch
        {
            AccDriverCategory.Silver => serverDetails.SilverClassification,
            AccDriverCategory.Gold => serverDetails.GoldClassification,
            AccDriverCategory.Platinum => serverDetails.PlatinumClassification,
            _ => serverDetails.BronzeClassification
        };
    }
}