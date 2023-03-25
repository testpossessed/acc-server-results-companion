using System;
using System.Collections.Generic;
using System.Linq;
using Acc.Server.Results.Companion.Core.Services;
using Acc.Server.Results.Companion.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Acc.Server.Results.Companion.Database;

internal static class DbRepository
{
    public static List<Session> GetSessionsForServer(int serverId)
    {
        var dbContext = GetDbContext();

        return dbContext.Sessions.Where(s => s.ServerId == serverId)
                        .OrderByDescending(s => s.TimeStamp)
                        .ToList();
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

    internal static List<ServerDetails> GetServers()
    {
        var dbContext = GetDbContext();

        return dbContext.Servers.ToList();
    }

    internal static void Init()
    {
        LogWriter.LogInfo("Applying any changes to the database");
        var dbContext = GetDbContext();
        dbContext.Database.Migrate();
    }

    private static AppDbContext GetDbContext()
    {
        return new AppDbContext();
    }
}