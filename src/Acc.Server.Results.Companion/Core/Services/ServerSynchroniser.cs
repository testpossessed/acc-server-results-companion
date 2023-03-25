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
            var filePaths = Directory.GetFiles(localFolderPath, "*.json");
            foreach(var filePath in filePaths)
            {
                if(filePath.Contains("_entrylist"))
                {
                    continue;
                }

                var json = NormalisedContent(filePath);
                var accSession = JsonConvert.DeserializeObject<AccSession>(json);
                if(accSession == null || accSession.Laps.Count == 0)
                {
                    continue;
                }

                var session = new Session
                              {
                                  FilePath = filePath,
                                  MetaData = accSession.MetaData,
                                  RaceWeekendIndex = accSession.RaceWeekendIndex,
                                  ServerId = serverId,
                                  ServerName = accSession.ServerName,
                                  SessionIndex = accSession.SessionIndex,
                                  SessionType = accSession.SessionType,
                                  TimeStamp = GetTimestampFromFileName(filePath),
                                  TrackName = accSession.TrackName
                              };
                DbRepository.AddSession(session);
            }
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

            return new DateTime(int.Parse(year), int.Parse(month), int.Parse(day), int.Parse(hour), int.Parse(minute), 0);
        }

        private static void SyncFtpServerFiles(ServerDetails serverDetails)
        {
            var url = new Uri(serverDetails.Address, UriKind.Absolute);

            var client = new FtpClient(url.Host, serverDetails.Username, serverDetails.Password, url.Port);
            client.ValidateCertificate += (client, args) => { args.Accept = true; };

            client.AutoConnect();

            var listItems = client.GetListing("/");
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