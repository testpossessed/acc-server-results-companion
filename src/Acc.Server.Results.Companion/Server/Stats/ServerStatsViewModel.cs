using System;
using Acc.Server.Results.Companion.Database;
using Acc.Server.Results.Companion.Database.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Acc.Server.Results.Companion.Server.Stats;

public class ServerStatsViewModel : ObservableObject
{
    private int driverCount;
    private int practiceLapCount;
    private int practiceSessionCount;
    private int qualiLapCount;
    private int qualiSessionCount;
    private int raceLapCount;
    private int raceSessionCount;
    private ServerDetails serverDetails;
    private int totalLapCount;
    private int totalInvalidLapCount;

    public int DriverCount
    {
        get => this.driverCount;
        set => this.SetProperty(ref this.driverCount, value);
    }

    public int PracticeLapCount
    {
        get => this.practiceLapCount;
        set => this.SetProperty(ref this.practiceLapCount, value);
    }

    public int PracticeSessionCount
    {
        get => this.practiceSessionCount;
        set => this.SetProperty(ref this.practiceSessionCount, value);
    }

    public int QualiLapCount
    {
        get => this.qualiLapCount;
        set => this.SetProperty(ref this.qualiLapCount, value);
    }

    public int QualiSessionCount
    {
        get => this.qualiSessionCount;
        set => this.SetProperty(ref this.qualiSessionCount, value);
    }

    public int RaceLapCount
    {
        get => this.raceLapCount;
        set => this.SetProperty(ref this.raceLapCount, value);
    }

    public int RaceSessionCount
    {
        get => this.raceSessionCount;
        set => this.SetProperty(ref this.raceSessionCount, value);
    }

    public int TotalLapCount
    {
        get => this.totalLapCount;
        set => this.SetProperty(ref this.totalLapCount, value);
    }

    public int TotalInvalidLapCount
    {
        get => this.totalInvalidLapCount;
        set => this.SetProperty(ref this.totalInvalidLapCount, value);
    }

    public void Refresh()
    {
        this.LoadServerStats();
    }

    public void SetServerDetails(ServerDetails serverDetails)
    {
        this.serverDetails = serverDetails;
        this.LoadServerStats();
    }

    private void LoadServerStats()
    {
        if(this.serverDetails == null)
        {
            return;
        }

        this.RaceSessionCount = DbRepository.GetRaceSessionCount(this.serverDetails.Id);
        this.QualiSessionCount = DbRepository.GetQualiSessionCount(this.serverDetails.Id);
        this.PracticeSessionCount = DbRepository.GetPracticeSessionCount(this.serverDetails.Id);

        this.RaceLapCount = DbRepository.GetRaceLapCount(this.serverDetails.Id);
        this.QualiLapCount = DbRepository.GetQualiLapCount(this.serverDetails.Id);
        this.PracticeLapCount = DbRepository.GetPracticeLapCount(this.serverDetails.Id);


        this.TotalLapCount = DbRepository.GetTotalLapCount(this.serverDetails.Id);
        this.TotalInvalidLapCount = DbRepository.GetTotalInvalidLapCount(this.serverDetails.Id);
        this.DriverCount = DbRepository.GetDriverCount();
    }
}