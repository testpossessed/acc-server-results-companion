using System;
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
    private int newResultFilesFound;
    private ServerDetails serverDetails;

    private string statusMessage;

    public ServerSyncDialogViewModel(Window window)
    {
        this.window = window;
    }

    public int NewResultFilesFound
    {
        get => this.newResultFilesFound;
        set => this.SetProperty(ref this.newResultFilesFound, value);
    }

    public ServerDetails ServerDetails
    {
        get => this.serverDetails;
        set => this.SetProperty(ref this.serverDetails, value);
    }

    private double progressValue;

    public double ProgressValue
    {
        get => this.progressValue;
        set => this.SetProperty(ref this.progressValue, value);
    }

    private double progressMaximum;

    public double ProgressMaximum
    {
        get => this.progressMaximum;
        set => this.SetProperty(ref this.progressMaximum, value);
    }

    public string StatusMessage
    {
        get => this.statusMessage;
        set => this.SetProperty(ref this.statusMessage, value);
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

    private void AddLeaderBoardLines(Session session, AccSession accSession)
    {
        this.StatusMessage = $"Importing Leader Board Lines from {session.DisplayName}...";
        var position = 1;
        foreach(var accLeaderBoardLine in accSession.SessionResult.LeaderBoardLines)
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
        var url = new Uri(serverDetails.Address, UriKind.Absolute);

        this.StatusMessage = $"Connecting to FTP server {serverDetails.Address}";

        var client = new AsyncFtpClient(url.Host,
            serverDetails.Username,
            serverDetails.Password,
            url.Port);
        client.ValidateCertificate += (client, args) => { args.Accept = true; };

       await client.AutoConnect();

        this.StatusMessage = "Getting list of results...";
        var localFolderPath =
            Path.Combine(PathProvider.DownloadedResultsFolderPath, serverDetails.Name);
        if(!Directory.Exists(localFolderPath))
        {
            Directory.CreateDirectory(localFolderPath!);
        }
        var localFiles = Directory.GetFiles(localFolderPath);
        var listItems = await client.GetListing(serverDetails.FtpFolderPath);

        var localResultCount = localFiles.Count(f => !f.Contains(EntryListKey));
        var remoteResultCount = listItems.Count(i => !i.Name.Contains(EntryListKey));
        this.NewResultFilesFound = remoteResultCount - localResultCount;

        this.ProgressMaximum = this.NewResultFilesFound * 2;
        this.ProgressValue = 0;

        foreach(var item in listItems)
        {
            if(item.Type != FtpObjectType.File)
            {
                continue;
            }

            var localFilePath = Path.Combine(PathProvider.DownloadedResultsFolderPath,
                serverDetails.Name,
                item.Name);
            if(File.Exists(localFilePath))
            {
                continue;
            }

            this.StatusMessage = $"Downloading {item.Name}...";
            await client.DownloadFile(localFilePath, item.FullName);
            this.ProgressValue++;
        }
    }

    private void SyncSessionsFromFolder(int serverId, string localFolderPath)
    {
        if(!Directory.Exists(localFolderPath))
        {
            Directory.CreateDirectory(localFolderPath!);
        }

        var filePaths = Directory.GetFiles(localFolderPath, "*.json");
        foreach(var filePath in filePaths)
        {
            if(filePath.Contains(EntryListKey) || DbRepository.SessionExists(filePath))
            {
                continue;
            }

            this.StatusMessage = $"Importing {Path.GetFileName(filePath)}...";
            var json = this.NormalisedContent(filePath);
            var accSession = JsonConvert.DeserializeObject<AccSession>(json);
            if(accSession == null || accSession.Laps.Count == 0)
            {
                this.StatusMessage = $"{Path.GetFileName(filePath)} did not contain any laps, ignoring it";
                continue;
            }

            var session = this.AddSession(serverId, filePath, accSession);

            this.AddLeaderBoardLines(session, accSession);
            this.ProgressValue++;
        }
    }
}