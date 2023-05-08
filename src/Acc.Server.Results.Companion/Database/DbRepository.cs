using System;
using System.Collections.Generic;
using System.Linq;
using Acc.Server.Results.Companion.Core;
using Acc.Server.Results.Companion.Core.Models;
using Acc.Server.Results.Companion.Core.Services;
using Acc.Server.Results.Companion.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Windows.Tools.Controls;
using Syncfusion.XlsIO.FormatParser.FormatTokens;

namespace Acc.Server.Results.Companion.Database;

internal static class DbRepository
{
    internal static void AddDriver(Driver driver)
    {
        var dbContext = GetDbContext();
        dbContext.Drivers.Add(driver);
        dbContext.SaveChanges();
    }

    internal static void AddLap(Lap lap)
    {
        var dbContext = GetDbContext();

        dbContext.Laps.Add(lap);
        dbContext.SaveChanges();
    }

    internal static void AddLeaderBoardLine(LeaderBoardLine leaderBoardLine)
    {
        var dbContext = GetDbContext();

        dbContext.LeaderBoardLines.Add(leaderBoardLine);
        dbContext.SaveChanges();
    }

    internal static void AddPenalty(Penalty penalty)
    {
        var dbContext = GetDbContext();

        dbContext.Penalties.Add(penalty);
        dbContext.SaveChanges();
    }

    internal static void AddServerDetails(ServerDetails serverDetails)
    {
        var dbContext = GetDbContext();

        dbContext.Servers.Add(serverDetails);
        dbContext.SaveChanges();
    }

    internal static Session AddSession(Session session)
    {
        var dbContext = GetDbContext();
        dbContext.Sessions.Add(session);
        dbContext.SaveChanges();
        return session;
    }

    internal static void ClearSessions()
    {
        var dbContext = GetDbContext();

        var sessions = dbContext.Sessions.ToList();
        foreach(var session in sessions)
        {
            dbContext.Sessions.Remove(session);
        }

        dbContext.SaveChanges();
    }

    internal static bool DriverExists(string steamId)
    {
        var dbContext = GetDbContext();
        return dbContext.Drivers.Any(d => d.Id == steamId);
    }

    internal static string GetCarNameByAccModelId(int modelId)
    {
        var dbContext = GetDbContext();

        var car = dbContext.Cars.FirstOrDefault(c => c.AccModelId == modelId);
        return car?.Name ?? "Unknown Car";
    }

    internal static Driver GetDriver(string playerId)
    {
        var dbContext = GetDbContext();

        return dbContext.Drivers.FirstOrDefault(d => d.Id == playerId);
    }

    internal static int GetDriverCount()
    {
        var dbContext = GetDbContext();

        return dbContext.Drivers.Count();
    }

    internal static List<Driver> GetDrivers()
    {
        var dbContext = GetDbContext();
        return dbContext.Drivers.OrderBy(d => d.FirstName)
                        .ThenBy(d => d.LastName)
                        .ToList();
    }

    internal static List<Lap> GetLaps(int sessionId)
    {
        var dbContext = GetDbContext();
        return dbContext.Laps.Where(l => l.SessionId == sessionId)
                        .ToList();
    }

    internal static List<LeaderBoardLine> GetLeaderBoardLines(int sessionId)
    {
        var dbContext = GetDbContext();

        return dbContext.LeaderBoardLines.Where(l => l.SessionId == sessionId)
                        .ToList();
    }

    internal static List<FastestLapViewModel> GetOverallFastestLaps(int serverId,
        string filterMode,
        bool includeInvalidLaps)
    {
        var sessionTypeFilter = GetSessionTypeFilter(filterMode);
        var dbContext = GetDbContext();

        var lapsQuery = from l in dbContext.Laps
                        join s in dbContext.Sessions on l.SessionId equals s.Id
                        where (serverId == 0 || s.ServerId == serverId) 
                              && (includeInvalidLaps || l.IsValid)
                              && (sessionTypeFilter == "A" || s.SessionType == sessionTypeFilter)
                        select new FastestLapViewModel
                               {
                                   Track = s.TrackName,
                                   Driver = l.Driver,
                                   Car = l.Car,
                                   LapTime = l.LapTime,
                                   Sector1Time = l.Sector1Time,
                                   Sector2Time = l.Sector2Time,
                                   Sector3Time = l.Sector3Time,
                                   LapTimeMs = l.LapTimeMs
                               };
        var fastestLaps = from l in lapsQuery
                          group l by l.Track
                          into g
                          select g.OrderBy(l => l.LapTimeMs)
                                  .First();

        return fastestLaps.ToList();
    }

    private static string GetSessionTypeFilter(string filterMode)
    {
        return filterMode switch
        {
            "Race" => "R",
            "Qualifying" => "Q",
            "Practice" => "FP",
            _ => "A"
        };
    }

    internal static List<Penalty> GetPenalties(int sessionId)
    {
        var dbContext = GetDbContext();
        return dbContext.Penalties.Where(p => p.SessionId == sessionId)
                        .ToList();
    }

    internal static int GetPracticeLapCount(int serverId)
    {
        return GetLapCount(serverId, "FP");
    }

    internal static int GetPracticeSessionCount(int serverId)
    {
        return GetSessionCount(serverId, "FP");
    }

    internal static List<string> GetProcessedEntryLists()
    {
        var dbContext = GetDbContext();

        return dbContext.Drivers.Select(d => d.LastUpdateFilePath)
                        .ToList();
    }

    internal static int GetQualiLapCount(int serverId)
    {
        return GetLapCount(serverId, "Q");
    }

    internal static int GetQualiSessionCount(int serverId)
    {
        return GetSessionCount(serverId, "Q");
    }

    internal static int GetRaceLapCount(int serverId)
    {
        return GetLapCount(serverId, "R");
    }

    internal static int GetRaceSessionCount(int serverId)
    {
        return GetSessionCount(serverId, "R");
    }

    internal static List<ServerDetails> GetServers()
    {
        var dbContext = GetDbContext();

        return dbContext.Servers.ToList();
    }

    internal static List<Session> GetSessionsForServer(int serverId)
    {
        var dbContext = GetDbContext();

        return dbContext.Sessions.Where(s => s.ServerId == serverId)
                        .OrderByDescending(s => s.TimeStamp)
                        .ToList();
    }

    internal static List<Session> GetRaceSessionsForServer(int serverId)
    {
        var dbContext = GetDbContext();

        return dbContext.Sessions.Include(s => s.Laps).Include(s => s.Penalties)
                        .Where(s => s.ServerId == serverId && s.SessionType == "R")
                        .OrderByDescending(s => s.TimeStamp)
                        .ToList();
    }

    internal static int GetTotalInvalidLapCount(int serverId)
    {
        var dbContext = GetDbContext();
        var query = from l in dbContext.Laps
                    join s in dbContext.Sessions on l.SessionId equals s.Id
                    where s.ServerId == serverId && !l.IsValid
                    select l;

        return query.Count();
    }

    internal static int GetLapCountForSession(int sessionId)
    {
        var dbContext = GetDbContext();

        var query = from l in dbContext.Laps
                    where l.SessionId == sessionId
                    group l by l.Driver
                    into g
                    select new
                           {
                               Driver = g.Key,
                               LapCount = g.Count()
                           };

        return query.Max(g => g.LapCount);

    }

    internal static int GetTotalLapCount(int serverId)
    {
        var dbContext = GetDbContext();
        var query = from l in dbContext.Laps
                    join s in dbContext.Sessions on l.SessionId equals s.Id
                    where s.ServerId == serverId
                    select l;

        return query.Count();
    }

    internal static string GetTrackNameByAccTrackId(string trackName)
    {
        var dbContext = GetDbContext();

        var track = dbContext.Tracks.FirstOrDefault(t => t.AccTrackId == trackName);

        return track?.Name ?? trackName;
    }

    internal static void Init()
    {
        LogWriter.LogInfo("Applying any changes to the database");
        var dbContext = GetDbContext();
        dbContext.Database.Migrate();
    }

    internal static bool SessionExists(string filePath)
    {
        var dbContext = GetDbContext();

        return dbContext.Sessions.Any(s => s.FilePath == filePath);
    }

    internal static void UpdateDriver(Driver driver)
    {
        var dbContext = GetDbContext();
        dbContext.Drivers.Update(driver);
        dbContext.SaveChanges();
    }

    internal static void UpdateServerDetails(ServerDetails serverDetails)
    {
        var dbContext = GetDbContext();

        dbContext.Servers.Update(serverDetails);
        dbContext.SaveChanges();
    }

    private static AppDbContext GetDbContext()
    {
        return new AppDbContext();
    }

    private static int GetLapCount(int serverId, string sessionType)
    {
        var dbContext = GetDbContext();
        var query = from l in dbContext.Laps
                    join s in dbContext.Sessions on l.SessionId equals s.Id
                    where s.SessionType == sessionType && s.ServerId == serverId
                    select l;

        return query.Count();
    }

    private static int GetSessionCount(int serverId, string sessionType)
    {
        var dbContext = GetDbContext();
        return dbContext.Sessions.Count(s => s.ServerId == serverId
                                             && s.SessionType == sessionType);
    }

    internal static List<Driver> GetDriversWithoutNames()
    {
        var dbContext = GetDbContext();
        return dbContext.Drivers.Where(d => string.IsNullOrWhiteSpace(d.FirstName)
                                            || string.IsNullOrWhiteSpace(d.LastName))
                        .ToList();
    }
}