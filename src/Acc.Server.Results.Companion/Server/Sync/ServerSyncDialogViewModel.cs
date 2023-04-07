using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Acc.Server.Results.Companion.AccEnums;
using Acc.Server.Results.Companion.AccModels;
using Acc.Server.Results.Companion.Core;
using Acc.Server.Results.Companion.Core.Services;
using Acc.Server.Results.Companion.Database;
using Acc.Server.Results.Companion.Database.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentFTP;
using Newtonsoft.Json;

namespace Acc.Server.Results.Companion.Server.Sync;

public class ServerSyncDialogViewModel : ObservableObject
{
    private readonly Window window;
    private string action;
    private int newResultFilesFound;
    private double progressMaximum;
    private double progressValue;
    private ServerDetails serverDetails;
    private string stage;

    public ServerSyncDialogViewModel(Window window)
    {
        this.window = window;
    }

    public string Action
    {
        get => this.action;
        set => this.SetProperty(ref this.action, value);
    }

    public int NewResultFilesFound
    {
        get => this.newResultFilesFound;
        set => this.SetProperty(ref this.newResultFilesFound, value);
    }

    public double ProgressMaximum
    {
        get => this.progressMaximum;
        set => this.SetProperty(ref this.progressMaximum, value);
    }

    public double ProgressValue
    {
        get => this.progressValue;
        set => this.SetProperty(ref this.progressValue, value);
    }

    public ServerDetails ServerDetails
    {
        get => this.serverDetails;
        set => this.SetProperty(ref this.serverDetails, value);
    }

    public string Stage
    {
        get => this.stage;
        set => this.SetProperty(ref this.stage, value);
    }

    public async Task Start(ServerDetails serverDetails)
    {
        this.ServerDetails = serverDetails;

        if(serverDetails.IsLocalFolder)
        {
            this.SyncFolder(serverDetails);
        }
        else
        {
            await this.SyncFtpServer(serverDetails);
        }

        this.window.Close();
        this.window.Owner.Activate();
    }

    private void AddLaps(Session session, AccSession accSession)
    {
        if(accSession?.Laps?.Any() is false)
        {
            return;
        }

        this.Action = $"Importing Laps from {session.DisplayName}...";

        foreach(var accSessionLap in accSession!.Laps!)
        {
            var car = accSession.GetCar(accSessionLap.CarId);
            var carName = DbRepository.GetCarNameByAccModelId(car.CarModel);
            var driver = accSession.GetDriver(accSessionLap.CarId, accSessionLap.DriverId);
            var dbDriver = DbRepository.GetDriver(driver.PlayerId);
            this.UpdateDriverDetails(dbDriver, driver);
            var lap = new Lap
                      {
                          Car = carName,
                          Driver = this.GetDriverName(driver, dbDriver),
                          DriverCategory = this.GetDriverCategory(dbDriver),
                          IsValid = accSessionLap.IsValidForBest,
                          LapTime = accSessionLap.GetLapTime(),
                          LapTimeMs = accSessionLap.LapTime,
                          NationalityCode = this.GetNationalityCode(car, dbDriver),
                          Nationality = this.GetNationality(car, dbDriver),
                          Sector1Time = accSessionLap.Sector1Time,
                          Sector1TimeMs = accSessionLap.Splits[0]
                                                       .ValidatedValue(),
                          Sector2Time = accSessionLap.Sector2Time,
                          Sector2TimeMs = accSessionLap.Splits[1]
                                                       .ValidatedValue(),
                          Sector3Time = accSessionLap.Sector3Time,
                          Sector3TimeMs = accSessionLap.Splits[2]
                                                       .ValidatedValue(),
                          SessionId = session.Id
                      };
            DbRepository.AddLap(lap);
        }
    }

    private void UpdateDriverDetails(Driver driver, AccDriver accDriver)
    {
        if(driver == null)
        {
            return;
        }

        var update = false;
        if(string.IsNullOrWhiteSpace(driver.FirstName) && !string.IsNullOrWhiteSpace(accDriver.FirstName))
        {
            driver.FirstName = accDriver.FirstName;
            update = true;
        }

        if(string.IsNullOrWhiteSpace(driver.LastName)
           && !string.IsNullOrWhiteSpace(accDriver.FirstName))
        {
            driver.LastName = accDriver.LastName;
            update = true;
        }

        if(string.IsNullOrWhiteSpace(driver.ShortName)
           && !string.IsNullOrWhiteSpace(accDriver.ShortName))
        {
            driver.ShortName = accDriver.ShortName;
            update = true;
        }

        if(update)
        {
            DbRepository.UpdateDriver(driver);
        }
    }

    private void AddLeaderBoardLines(Session session, AccSession accSession)
    {
        if(accSession.SessionResult.LeaderBoardLines?.Any() is false)
        {
            return;
        }

        this.Action = $"Importing Leader Board Lines from {session.DisplayName}...";
        var position = 1;
        foreach(var accLeaderBoardLine in accSession.SessionResult.LeaderBoardLines!)
        {
            var accTiming = accLeaderBoardLine.Timing;
            var currentDriver = accLeaderBoardLine.CurrentDriver;
            var accCar = accLeaderBoardLine.Car;
            var dbDriver = DbRepository.GetDriver(currentDriver.PlayerId);

            var leaderBoardLine = new LeaderBoardLine
                                  {
                                      AverageLapTime = accTiming.AverageLapTime,
                                      AverageLapTimeMs =
                                          accTiming.AverageLapTimeMs.ValidatedValue(),
                                      BestSector1Time = accTiming.BestSector1,
                                      BestSector1TimeMs = accTiming.BestSplits[0]
                                          .ValidatedValue(),
                                      BestSector2Time = accTiming.BestSector2,
                                      BestSector2TimeMs = accTiming.BestSplits[1]
                                          .ValidatedValue(),
                                      BestSector3Time = accTiming.BestSector3,
                                      BestSector3TimeMs = accTiming.BestSplits[2]
                                          .ValidatedValue(),
                                      BestLapTime = accTiming.BestLapTime,
                                      BestLapTimeMs = accTiming.BestLap,
                                      CarName =
                                          DbRepository.GetCarNameByAccModelId(accCar.CarModel),
                                      CarClass = accCar.CarGroup,
                                      DriverName = this.GetDriverName(currentDriver, dbDriver),
                                      DriverShortName = currentDriver.ShortName,
                                      DriverCategory = this.GetDriverCategory(dbDriver),
                                      MissingMandatoryPitStop =
                                          accLeaderBoardLine.MissingMandatoryPitstop,
                                      NationalityCode = this.GetNationalityCode(accCar, dbDriver),
                                      Nationality = this.GetNationality(accCar, dbDriver),
                                      Position = position++,
                                      SessionId = session.Id,
                                      TeamName = accCar.TeamName
                                  };
            DbRepository.AddLeaderBoardLine(leaderBoardLine);
        }
    }

    private void AddPenalties(Session session, AccSession accSession)
    {
        if(accSession?.Penalties?.Any() is false && accSession?.PostRacePenalties?.Any() is false)
        {
            return;
        }

        this.Action = $"Importing Penalties from {session.DisplayName}...";

        foreach(var accPenalty in accSession!.Penalties!)
        {
            var car = accSession.GetCar(accPenalty.CarId);
            var carName = DbRepository.GetCarNameByAccModelId(car.CarModel);
            var driver = accSession.GetDriver(accPenalty.CarId, accPenalty.DriverIndex);
            var dbDriver = DbRepository.GetDriver(driver.PlayerId);
            var penalty = new Penalty
                          {
                              Car = carName,
                              Driver = this.GetDriverName(driver, dbDriver),
                              DriverCategory = this.GetDriverCategory(dbDriver),
                              ClearedOnLap = accPenalty.ClearedInLap,
                              IsPostRacePenalty = false,
                              NationalityCode = this.GetNationalityCode(car, dbDriver),
                              Nationality = this.GetNationality(car, dbDriver),
                              PenaltyCode = accPenalty.Penalty,
                              PenaltyValue = accPenalty.PenaltyValue,
                              Reason = accPenalty.Reason,
                              SessionId = session.Id,
                              ViolationOnLap = accPenalty.ViolationInLap
                          };
            DbRepository.AddPenalty(penalty);
        }

        foreach(var accPenalty in accSession!.PostRacePenalties!)
        {
            var car = accSession.GetCar(accPenalty.CarId);
            var carName = DbRepository.GetCarNameByAccModelId(car.CarModel);
            var driver = accSession.GetDriver(accPenalty.CarId, accPenalty.DriverIndex);
            var dbDriver = DbRepository.GetDriver(driver.PlayerId);
            var penalty = new Penalty
                          {
                              Car = carName,
                              Driver = this.GetDriverName(driver, dbDriver),
                              DriverCategory = this.GetDriverCategory(dbDriver),
                              ClearedOnLap = accPenalty.ClearedInLap,
                              IsPostRacePenalty = true,
                              NationalityCode = this.GetNationalityCode(car, dbDriver),
                              Nationality = this.GetNationality(car, dbDriver),
                              PenaltyCode = accPenalty.Penalty,
                              PenaltyValue = accPenalty.PenaltyValue,
                              Reason = accPenalty.Reason,
                              SessionId = session.Id,
                              ViolationOnLap = accPenalty.ViolationInLap
                          };
            DbRepository.AddPenalty(penalty);
        }
    }

    private Session AddSession(int serverId, string filePath, AccSession accSession)
    {
        var session = new Session
                      {
                          BestLapMs = accSession.SessionResult.BestLap.ValidatedValue(),
                          BestSector1Ms = accSession.SessionResult.BestSplits[0]
                                                    .ValidatedValue(),
                          BestSector2Ms = accSession.SessionResult.BestSplits[1]
                                                    .ValidatedValue(),
                          BestSector3Ms = accSession.SessionResult.BestSplits[2]
                                                    .ValidatedValue(),
                          FilePath = filePath,
                          IsWetSession = accSession.SessionResult.IsWetSession,
                          MetaData = accSession.MetaData,
                          RaceWeekendIndex = accSession.RaceWeekendIndex,
                          ServerId = serverId,
                          ServerName = accSession.ServerName,
                          SessionIndex = accSession.SessionIndex,
                          SessionType = accSession.SessionType,
                          TimeStamp = this.GetTimestampFromFileName(filePath),
                          TrackName = DbRepository.GetTrackNameByAccTrackId(accSession.TrackName)
                      };

        DbRepository.AddSession(session);
        return session;
    }

    private string GetDriverCategory(Driver dbDriver)
    {
        var category = dbDriver?.DriverCategoryCode ?? 0;
        return ((AccDriverCategory)category).ToString();
    }

    private string GetDriverFirstName(Driver driver)
    {
        if(!string.IsNullOrWhiteSpace(driver.FirstNameOverride))
        {
            return driver.FirstNameOverride[..1];
        }

        return !string.IsNullOrWhiteSpace(driver.FirstName)? driver.FirstName[..1]: "**";
    }

    private string GetDriverLastName(Driver driver)
    {
        if(!string.IsNullOrWhiteSpace(driver.LastNameOverride))
        {
            return driver.LastNameOverride;
        }

        return !string.IsNullOrWhiteSpace(driver.LastName)? driver.LastName: "**";
    }

    private string GetDriverName(AccDriver accDriver, Driver driver)
    {
        return driver == null
                   ? accDriver.DisplayName
                   : $"{this.GetDriverFirstName(driver)}. {this.GetDriverLastName(driver)}";
    }

    private List<FtpListItem> GetFilesToDownload(IEnumerable<string> localFiles,
        IEnumerable<FtpListItem> listItems)
    {
        var localFileNames = localFiles.Select(Path.GetFileName)
                                       .ToList();
        return listItems
               .Where(i => i.Type == FtpObjectType.File && !localFileNames.Contains(i.Name))
               .ToList();
    }

    private string GetNationality(AccCar accCar, Driver driver)
    {
        return ((AccNationality)this.GetNationalityCode(accCar, driver)).ToFriendlyName();
    }

    private int GetNationalityCode(AccCar accCar, Driver driver)
    {
        if(driver == null)
        {
            return accCar.Nationality;
        }

        return driver.NationalityCodeOverride ?? driver.NationalityCode;
    }

    private DateTime GetTimestampFromFileName(string filePath)
    {
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var elements = fileName.Split('_', StringSplitOptions.RemoveEmptyEntries);

        var year = elements[0][..2];
        var month = elements[0]
            .Substring(2, 2);
        var day = elements[0]
            .Substring(4, 2);

        var hour = elements[1][..2];
        var minute = elements[1]
            .Substring(2, 2);
        var second = elements[1]
            .Substring(4, 2);

        return new DateTime(int.Parse(year) + 2000,
            int.Parse(month),
            int.Parse(day),
            int.Parse(hour),
            int.Parse(minute),
            int.Parse(second));
    }

    private void ImportEntryList(string json, string filePath)
    {
        var entryList = JsonConvert.DeserializeObject<AccEntryList>(json);
        var drivers = entryList.Entries.SelectMany(l => l.Drivers);
        foreach(var driver in drivers)
        {
            var dbDriver = DbRepository.GetDriver(driver.PlayerId) ?? new Driver
                               {
                                   DriverCategoryCode = (int)driver.DriverCategory,
                                   LastUpdateFilePath = filePath,
                                   FirstName = driver.FirstName,
                                   LastName = driver.LastName,
                                   Nationality = driver.Nationality.ToFriendlyName(),
                                   NationalityCode = (int)driver.DriverCategory,
                                   ShortName = driver.ShortName,
                                   IsImported = true
                               };

            if(string.IsNullOrWhiteSpace(dbDriver.Id))
            {
                dbDriver.Id = driver.PlayerId;
                DbRepository.AddDriver(dbDriver);
            }
            else
            {
                if(this.SyncDriverDetails(dbDriver, driver))
                {
                    dbDriver.LastUpdateFilePath = filePath;
                    DbRepository.UpdateDriver(dbDriver);
                }
            }
        }
    }

    private bool SyncDriverDetails(Driver driver, AccEntryListDriver entryListDriver)
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

    private void ImportSession(int serverId, string json, string filePath)
    {
        var accSession = JsonConvert.DeserializeObject<AccSession>(json);
        if(accSession == null || accSession.Laps?.Any() is false)
        {
            this.Action = $"{Path.GetFileName(filePath)} did not contain any laps, ignoring it";
            return;
        }

        var session = this.AddSession(serverId, filePath, accSession);

        this.AddLeaderBoardLines(session, accSession);
        this.AddLaps(session, accSession);
        this.AddPenalties(session, accSession);
        this.ProgressValue++;
    }

    private string NormalisedContent(string filePath)
    {
        var content = File.ReadAllText(filePath, Encoding.UTF8);

        return content.Replace(Environment.NewLine, "")
                      .Replace("\0", "")
                      .Replace("\n", "");
    }

    private void SyncFolder(ServerDetails serverDetails)
    {
        this.SyncSessionsFromFolder(serverDetails.Id, this.serverDetails.Address);
    }

    private async Task SyncFtpServer(ServerDetails serverDetails)
    {
        await this.SyncFtpServerFiles(serverDetails);
        var localFolderPath = Path.Combine(
            PathProvider.DownloadedResultsFolderPath,
            serverDetails.Name);
        this.SyncSessionsFromFolder(serverDetails.Id, localFolderPath);
    }

    private async Task SyncFtpServerFiles(ServerDetails serverDetails)
    {
        this.Stage = "Download Result Files";

        var url = new Uri(serverDetails.Address, UriKind.Absolute);

        this.Action = $"Connecting to FTP server {serverDetails.Address}";

        var client = new AsyncFtpClient(url.Host,
            serverDetails.Username,
            serverDetails.Password,
            url.Port);
        client.ValidateCertificate += (client, args) => { args.Accept = true; };

        await client.AutoConnect();

        this.Action = "Getting list of results...";
        var localFolderPath =
            Path.Combine(PathProvider.DownloadedResultsFolderPath, serverDetails.Name);
        if(!Directory.Exists(localFolderPath))
        {
            Directory.CreateDirectory(localFolderPath!);
        }

        var localFiles = Directory.GetFiles(localFolderPath);
        var listItems = await client.GetListing(serverDetails.FtpFolderPath);

        var itemsToDownload = this.GetFilesToDownload(localFiles, listItems);

        var itemsToDownloadCount = itemsToDownload.Count();
        this.NewResultFilesFound = itemsToDownloadCount;

        this.ProgressMaximum = itemsToDownloadCount;
        this.ProgressValue = 0;

        foreach(var item in itemsToDownload)
        {
            var localFilePath = Path.Combine(PathProvider.DownloadedResultsFolderPath,
                serverDetails.Name,
                item.Name);

            this.Action = $"Downloading {item.Name}...";
            await client.DownloadFile(localFilePath, item.FullName);
            this.ProgressValue++;
        }

        await client.Disconnect();
        client.Dispose();
    }

    private void SyncSessionsFromFolder(int serverId, string localFolderPath)
    {
        this.Stage = "Import New Files";

        if(!Directory.Exists(localFolderPath))
        {
            Directory.CreateDirectory(localFolderPath!);
        }

        var sessions = DbRepository.GetSessionsForServer(serverId);
        var entryListFilePaths = DbRepository.GetProcessedEntryLists();

        var sessionFilePaths = sessions.Select(s => s.FilePath)
                                       .ToList();

        var filePaths = Directory.GetFiles(localFolderPath, "*.json");
        var newFilePaths = filePaths
                           .Where(p => !entryListFilePaths.Contains(p)
                                       && !sessionFilePaths.Contains(p))
                           .ToList();

        var newFilesToProcess = newFilePaths.Select(p => new ImportFileInfo(p))
                                            .OrderBy(i => i.TimeStamp)
                                            .ThenBy(i => i.SortIndex)
                                            .ToList();

        this.ProgressValue = 0;
        this.ProgressMaximum = newFilePaths.Count;
        foreach(var importFileInfo in newFilesToProcess)
        {
            this.Action = $"Importing {Path.GetFileName(importFileInfo.FilePath)}...";
            LogWriter.LogDebug(this.Action);
            var json = this.NormalisedContent(importFileInfo.FilePath);

            if(importFileInfo.IsEntryList)
            {
                this.ImportEntryList(json, importFileInfo.FilePath);
                continue;
            }

            this.ImportSession(serverId, json, importFileInfo.FilePath);
        }
    }
}