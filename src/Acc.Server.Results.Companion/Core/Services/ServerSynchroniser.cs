using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Acc.Server.Results.Companion.AccModels;
using Acc.Server.Results.Companion.Database;
using Acc.Server.Results.Companion.Database.Entities;
using FluentFTP;
using Newtonsoft.Json;

namespace Acc.Server.Results.Companion.Core.Services
{
    internal static class ServerSynchroniser
    {
        internal static void Sync(ServerDetails serverDetails)
        {
            if(serverDetails.IsLocalFolder)
            {
                SyncFolder(serverDetails);
            }
            else
            {
                SyncFtpServer(serverDetails);
            }
        }

        private static Task SyncFolder(ServerDetails server)
        {
            return Task.CompletedTask;
        }

        private static void SyncFtpServer(ServerDetails serverDetails)
        {
            SyncFtpServerFiles(serverDetails);
            var localFolderPath = Path.Combine(
                PathProvider.DownloadedResultsFolderPath,
                serverDetails.Name);
            SyncSessionsFromFolder(serverDetails.Id, localFolderPath);
        }

        private static void SyncSessionsFromFolder(int serverId, string localFolderPath)
        {
            if(!Directory.Exists(localFolderPath))
            {
                Directory.CreateDirectory(localFolderPath!);
            }

            var filePaths = Directory.GetFiles(localFolderPath, "*.json");
            foreach(var filePath in filePaths)
            {
                if(filePath.Contains("_entrylist") || DbRepository.SessionExists(filePath))
                {
                    continue;
                }

                var json = NormalisedContent(filePath);
                var accSession = JsonConvert.DeserializeObject<AccSession>(json);
                if(accSession == null || accSession.Laps.Count == 0)
                {
                    continue;
                }

                var session = AddSession(serverId, filePath, accSession);

                AddLeaderBoardLines(session, accSession);
            }
        }

        private static void AddLeaderBoardLines(Session session, AccSession accSession)
        {
            var position = 1;
            foreach(var accLeaderBoardLine in accSession.SessionResult.LeaderBoardLines)
            {
                var accTiming = accLeaderBoardLine.Timing;
                var currentDriver = accLeaderBoardLine.CurrentDriver;
                var accCar = accLeaderBoardLine.Car;
                var leaderBoardLine = new LeaderBoardLine
                                      {
                                          AverageLapTime = accTiming.AverageLapTime,
                                          AverageLapTimeMs = accTiming.AverageLapTimeMs.ValidatedValue(),
                                          BestSector1Time = accTiming.BestSector1,
                                          BestSector1TimeMs = accTiming.BestSplits[0].ValidatedValue(),
                                          BestSector2Time = accTiming.BestSector2,
                                          BestSector2TimeMs =
                                              accTiming.BestSplits[1].ValidatedValue(),
                                          BestSector3Time = accTiming.BestSector3,
                                          BestSector3TimeMs =
                                              accTiming.BestSplits[2].ValidatedValue(),
                                          BestLapTime = accTiming.BestLapTime,
                                          BestLapTimeMs = accTiming.BestLap,
                                          CarName = DbRepository.GetCarNameByAccModelId(
                                              accCar.CarModel),
                                          CarClass = accCar.CarGroup,
                                          DriverName = currentDriver.DisplayName,
                                          DriverShortName = currentDriver.ShortName,
                                          MissingMandatoryPitStop = accLeaderBoardLine.MissingMandatoryPitstop,
                                          Position = position++,
                                          SessionId = session.Id,
                                          TeamName = accCar.TeamName
                                      };
                DbRepository.AddLeaderBoardLine(leaderBoardLine);
            }
        }

        private static Session AddSession(int serverId, string filePath, AccSession accSession)
        {
            var session = new Session
                   {
                       BestLapMs = accSession.SessionResult.BestLap.ValidatedValue(),
                       BestSector1Ms = accSession.SessionResult.BestSplits[0].ValidatedValue(),
                       BestSector2Ms = accSession.SessionResult.BestSplits[1].ValidatedValue(),
                       BestSector3Ms = accSession.SessionResult.BestSplits[2].ValidatedValue(),
                       FilePath = filePath,
                       IsWetSession = accSession.SessionResult.IsWetSession,
                       MetaData = accSession.MetaData,
                       RaceWeekendIndex = accSession.RaceWeekendIndex,
                       ServerId = serverId,
                       ServerName = accSession.ServerName,
                       SessionIndex = accSession.SessionIndex,
                       SessionType = accSession.SessionType,
                       TimeStamp = GetTimestampFromFileName(filePath),
                       TrackName = DbRepository.GetTrackNameByAccTrackId(accSession.TrackName)
                   };


            DbRepository.AddSession(session);
            return session;
        }

        private static DateTime GetTimestampFromFileName(string filePath)
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

            return new DateTime(int.Parse(year) + 2000, int.Parse(month), int.Parse(day), int.Parse(hour), int.Parse(minute), int.Parse(second));
        }

        private static void SyncFtpServerFiles(ServerDetails serverDetails)
        {
            var url = new Uri(serverDetails.Address, UriKind.Absolute);

            var client = new FtpClient(url.Host, serverDetails.Username, serverDetails.Password, url.Port);
            client.ValidateCertificate += (client, args) => { args.Accept = true; };

            client.AutoConnect();

            var listItems = client.GetListing(serverDetails.FtpFolderPath);
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

                client.DownloadFile(localFilePath, item.FullName);
            }
        }

        private static string NormalisedContent(string filePath)
        {
            var content = File.ReadAllText(filePath, Encoding.UTF8);

            return content.Replace(Environment.NewLine, "")
                          .Replace("\0", "")
                          .Replace("\n", "");
        }
    }
}