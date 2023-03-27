using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    private const string EntryListKey = "_entrylist";

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
            await this.SyncFolder(serverDetails);
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
            var driverName = driver.DisplayName;
            var lap = new Lap
                      {
                          Car = carName,
                          Driver = driverName,
                          IsValid = accSessionLap.IsValidForBest,
                          LapTime = accSessionLap.GetLapTime(),
                          LapTimeMs = accSessionLap.LapTime,
                          Sector1Time = accSessionLap.Sector1Time,
                          Sector1TimeMs = accSessionLap.Splits[0].ValidatedValue(),
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
            var driverName = driver.DisplayName;
            var penalty = new Penalty
                      {
                          Car = carName,
                          Driver = driverName,
                          ClearedOnLap = accPenalty.ClearedInLap,
                          IsPostRacePenalty = false,
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
            var driverName = driver.DisplayName;
            var penalty = new Penalty
                          {
                              Car = carName,
                              Driver = driverName,
                              ClearedOnLap = accPenalty.ClearedInLap,
                              IsPostRacePenalty = true,
                              PenaltyCode = accPenalty.Penalty,
                              PenaltyValue = accPenalty.PenaltyValue,
                              Reason = accPenalty.Reason,
                              SessionId = session.Id,
                              ViolationOnLap = accPenalty.ViolationInLap
                          };
            DbRepository.AddPenalty(penalty);
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
                                      DriverName = currentDriver.DisplayName,
                                      DriverShortName = currentDriver.ShortName,
                                      MissingMandatoryPitStop =
                                          accLeaderBoardLine.MissingMandatoryPitstop,
                                      Position = position++,
                                      SessionId = session.Id,
                                      TeamName = accCar.TeamName
                                  };
            DbRepository.AddLeaderBoardLine(leaderBoardLine);
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

    private List<FtpListItem> GetFilesToDownload(IEnumerable<string> localFiles,
        IEnumerable<FtpListItem> listItems)
    {
        var localFileNames = localFiles.Select(Path.GetFileName)
                                       .ToList();
        return listItems
               .Where(i => i.Type == FtpObjectType.File && !localFileNames.Contains(i.Name))
               .ToList();
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

    private string NormalisedContent(string filePath)
    {
        var content = File.ReadAllText(filePath, Encoding.UTF8);

        return content.Replace(Environment.NewLine, "")
                      .Replace("\0", "")
                      .Replace("\n", "");
    }

    private Task SyncFolder(ServerDetails server)
    {
        return Task.CompletedTask;
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
        this.Stage = "Import New Results";

        if(!Directory.Exists(localFolderPath))
        {
            Directory.CreateDirectory(localFolderPath!);
        }

        var filePaths = Directory.GetFiles(localFolderPath, "*.json");
        var sessions = DbRepository.GetSessionsForServer(serverId);
        var sessionFilePaths = sessions.Select(s => s.FilePath)
                                       .ToList();

        var newFilePaths = filePaths.Where(p => !p.Contains(EntryListKey) && !sessionFilePaths.Contains(p))
                                    .ToList();
        this.ProgressValue = 0;
        this.ProgressMaximum = newFilePaths.Count;
        foreach(var filePath in newFilePaths)
        {
            this.Action = $"Importing {Path.GetFileName(filePath)}...";
            var json = this.NormalisedContent(filePath);
            var accSession = JsonConvert.DeserializeObject<AccSession>(json);
            if(accSession == null || accSession.Laps?.Any() is false)
            {
                this.Action = $"{Path.GetFileName(filePath)} did not contain any laps, ignoring it";
                continue;
            }

            var session = this.AddSession(serverId, filePath, accSession);

            this.AddLeaderBoardLines(session, accSession);
            this.AddLaps(session, accSession);
            this.AddPenalties(session, accSession);
            this.ProgressValue++;
        }
    }
}