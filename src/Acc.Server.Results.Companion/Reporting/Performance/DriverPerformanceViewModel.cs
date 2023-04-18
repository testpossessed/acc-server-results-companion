using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Acc.Server.Results.Companion.Core.Models;
using Acc.Server.Results.Companion.Database;
using Acc.Server.Results.Companion.Database.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Acc.Server.Results.Companion.Reporting.Performance;

public class DriverPerformanceViewModel : ObservableObject
{
    private ServerListItem selectedServer;
    public ObservableCollection<ServerListItem> Servers { get; } = new();
    public ObservableCollection<DriverPerformanceItem> DriverPerformance { get; } = new();

    public DriverPerformanceViewModel()
    {
        this.LoadServers();
    }

    public ServerListItem SelectedServer
    {
        get => this.selectedServer;
        set
        {
            this.SetProperty(ref this.selectedServer, value);
            this.LoadDriverPerformance();
        }
    }

    private void LoadDriverPerformance()
    {
        this.DriverPerformance.Clear();
        var results = new List<DriverPerformanceItem>();

        var drivers = DbRepository.GetDrivers();
        var raceSessions = DbRepository.GetRaceSessionsForServer(this.SelectedServer.Id);
        foreach(var driver in drivers)
        {
            var driverPerformance = new DriverPerformanceItem
                                   {
                                       DriverName = GetDriverName(driver)
                                   };

            if(string.IsNullOrWhiteSpace(driverPerformance.DriverName) || string.IsNullOrWhiteSpace(driverPerformance.DriverName.Trim()))
            {
                continue;
            }

            results.Add(driverPerformance);
                                                                                             
            var driverSessions = GetDriverSessions(raceSessions, driverPerformance);

            if(!driverSessions.Any() || driverSessions.Count < 3)
            {
                continue;
            }

            var consistencyCalculations = new List<double>();
            foreach(var driverSession in driverSessions)
            {
                var driverLaps = driverSession
                                 .Laps.Where(l => l.Driver == driverPerformance.DriverName)
                                 .ToList();

                if(!driverLaps.Any())
                {
                    continue;
                }

                var validLaps = driverLaps.Where(l => l.IsValid)
                                          .ToList();
                var lapTimes = validLaps.Where(l => l.LapTimeMs < 300000).Select(l => l.LapTimeMs).ToList();
                if(lapTimes.Any())
                {
                    var averageLapTime = lapTimes.Average();
                    var stdDev = Math.Sqrt(lapTimes.Average(v => Math.Pow(v - averageLapTime, 2)));
                    var consistency = stdDev * 100 / averageLapTime;
                    consistencyCalculations.Add(consistency);
                }

                var validLapCount = validLaps.Count();
                var invalidLapCount = driverLaps.Count() - validLapCount;
                driverPerformance.ValidLapCount+= validLapCount;
                driverPerformance.InvalidLapCount+= invalidLapCount;

            }

            if(consistencyCalculations.Any())
            {
                driverPerformance.Consistency = Math.Round(consistencyCalculations.Average(), 2);
                driverPerformance.ConsistencyDisplay = $"{100 - driverPerformance.Consistency:F}%";
            }

            driverPerformance.ValidRatio = Math.Round(
                (double)driverPerformance.ValidLapCount / driverPerformance.InvalidLapCount,
                2);
            driverPerformance.ValidRatioDisplay = $"{driverPerformance.ValidRatio:F}:1";

        }

        foreach(var result in results.OrderByDescending(r => r.ValidRatio).ThenBy(r => r.Consistency))
        {
            this.DriverPerformance.Add(result);
        }
    }

    private static List<Session> GetDriverSessions(List<Session> raceSessions, DriverPerformanceItem driverPerformance)
    {
        var driverSessions = new List<Session>();

        foreach(var raceSession in raceSessions)
        {
            var raceLapCount = DbRepository.GetLapCountForSession(raceSession.Id);

            if(raceLapCount < 10)
            {
                continue; 
            }

            var driverLapCount =
                raceSession.Laps.Count(l => l.Driver == driverPerformance.DriverName);

            if(driverLapCount < (raceLapCount * .25))
            {
                continue;
            }

            driverSessions.Add(raceSession);
        }

        return driverSessions;
    }

    private static string GetDriverName(Driver driver)
    {
        var firstName = string.IsNullOrWhiteSpace(driver.FirstNameOverride)
                            ? driver.FirstName
                            : driver.FirstNameOverride;
        var lastName = string.IsNullOrWhiteSpace(driver.LastNameOverride)? driver.LastName : driver.LastNameOverride;
        if(string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            return null;
        }
        var firstInitial = string.IsNullOrWhiteSpace(firstName)? string.Empty: $"{firstName[..1]}.";
        return $"{firstInitial} {lastName}";
    }

    private void LoadServers()
    {
        this.Servers.Clear();

        var servers = DbRepository.GetServers();
        foreach(var server in servers)
        {
            this.Servers.Add(new ServerListItem
                             {
                                 Id = server.Id,
                                 Name = server.Name
                             });
        }

        this.SelectedServer = this.Servers[0];
    }
}