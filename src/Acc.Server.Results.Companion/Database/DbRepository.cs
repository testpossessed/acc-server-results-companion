using System;
using System.Collections.Generic;
using System.Linq;
using Acc.Server.Results.Companion.Core.Services;
using Acc.Server.Results.Companion.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Acc.Server.Results.Companion.Database;

internal static class DbRepository
{
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

    internal static string GetCarNameByAccModelId(int modelId)
    {
        var dbContext = GetDbContext();

        var car = dbContext.Cars.FirstOrDefault(c => c.AccModelId == modelId);
        return car?.Name ?? "Unknown Car";
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

    internal static List<Penalty> GetPenalties(int sessionId)
    {
        var dbContext = GetDbContext();
        return dbContext.Penalties.Where(p => p.SessionId == sessionId)
                        .ToList();
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

    private static AppDbContext GetDbContext()
    {
        return new AppDbContext();
    }
}