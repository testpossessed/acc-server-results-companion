using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Acc.Server.Results.Companion.AccModels;
using Acc.Server.Results.Companion.Database;
using Acc.Server.Results.Companion.Database.Entities;
using Newtonsoft.Json;

namespace Acc.Server.Results.Companion.Core.Services;

internal static class DriverNameRepairer
{
    internal static void Repair(ServerDetails serverDetails)
    {
        var driversWithoutNames = DbRepository.GetDriversWithoutNames();
        if(!driversWithoutNames.Any())
        {
            return;
        }

        var folderPath = serverDetails.IsLocalFolder
                             ? serverDetails.Address
                             : Path.Combine(PathProvider.DownloadedResultsFolderPath,
                                 serverDetails.Name);

        var directoryInfo = new DirectoryInfo(folderPath);
        var entryListFiles = directoryInfo.GetFiles("*_entrylist.json")
                                          .OrderByDescending(f => f.CreationTimeUtc)
                                          .ToList();
        var entryLists = new List<AccEntryList>();

        foreach(var entryListFile in entryListFiles)
        {
            var json = NormalisedContent(entryListFile.FullName);
            try
            {
                var entryList = JsonConvert.DeserializeObject<AccEntryList>(json);
                entryLists.Add(entryList);
            }
            catch(Exception exception)
            {
                LogWriter.LogError(exception,
                    $"Unexpected error deserialising entry list {entryListFile.FullName}");
            }
        }

        foreach(var driver in driversWithoutNames)
        {
            foreach(var entryList in entryLists)
            {
                var entryListDrivers = entryList.Entries.SelectMany(l => l.Drivers);
                var entryListDriver = entryListDrivers.FirstOrDefault(d => d.PlayerId == driver.Id);
                if(entryListDriver == null || entryListDriver.IsSystemAdmin || (string.IsNullOrWhiteSpace(entryListDriver.FirstName) && string.IsNullOrWhiteSpace(entryListDriver.LastName)))
                {
                    continue;
                }

                if(SyncDriverDetails(driver, entryListDriver))
                {
                    DbRepository.UpdateDriver(driver);
                    break;
                }
            }
        }
    }


    private static string NormalisedContent(string filePath)
    {
        var content = File.ReadAllText(filePath, Encoding.UTF8);

        return content.Replace(Environment.NewLine, "")
                      .Replace("\0", "")
                      .Replace("\n", "");
    }

    private static bool SyncDriverDetails(Driver driver, AccEntryListDriver entryListDriver)
    {
        var updated = false;

        var driverCategoryCode = (int)entryListDriver.DriverCategory;
        if(driver.DriverCategoryCode != driverCategoryCode)
        {
            driver.DriverCategoryCode = (int)entryListDriver.DriverCategory;
            updated = true;
        }

        if(driver.FirstName != entryListDriver.FirstName)
        {
            driver.FirstName = entryListDriver.FirstName;
            updated = true;
        }

        if(driver.LastName != entryListDriver.LastName)
        {
            driver.LastName = entryListDriver.LastName;
            updated = true;
        }

        var nationalityCode = (int)entryListDriver.Nationality;
        if(driver.NationalityCode != nationalityCode)
        {
            driver.NationalityCode = nationalityCode;
            updated = true;
        }

        var nationality = entryListDriver.Nationality.ToFriendlyName();
        if(driver.Nationality != nationality)
        {
            driver.Nationality = nationality;
            updated = true;
        }

        if(driver.ShortName != entryListDriver.ShortName)
        {
            driver.ShortName = entryListDriver.ShortName;
            updated = true;
        }

        return updated;
    }
}