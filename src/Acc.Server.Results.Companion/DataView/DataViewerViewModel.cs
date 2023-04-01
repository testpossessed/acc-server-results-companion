using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Acc.Server.Results.Companion.Core.Models;
using Acc.Server.Results.Companion.Database;
using Acc.Server.Results.Companion.Database.Entities;
using Acc.Server.Results.Companion.Server.Sync;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Acc.Server.Results.Companion.DataView;

public class DataViewerViewModel : ObservableObject
{
    internal event EventHandler SynchronisationCompleted;

    private Session selectedSession;
    private ServerDetails serverDetails;

    public ObservableCollection<LapViewModel> Laps { get; } = new();
    public ObservableCollection<LeaderBoardLineViewModel> LeaderBoardLines { get; } = new();
    public ObservableCollection<PenaltyViewModel> Penalties { get; } = new();
    public ObservableCollection<Session> Sessions { get; } = new();

    public Session SelectedSession
    {
        get => this.selectedSession;
        set
        {
            this.SetProperty(ref this.selectedSession, value);
            this.LoadSession();
        }
    }

    public void Refresh()
    {
        this.LoadSession();
    }

    internal void SetServerDetails(ServerDetails serverDetails)
    {
        this.serverDetails = serverDetails;
        this.LoadServerSessions();
    }

    private void LoadLaps()
    {
        this.Laps.Clear();
        if(this.SelectedSession == null)
        {
            return;
        }

        var sessionLaps = DbRepository.GetLaps(this.SelectedSession.Id);

        foreach(var lap in sessionLaps)
        {
            this.Laps.Add(new LapViewModel(lap, this.serverDetails));
        }
    }

    private void LoadLeaderBoardLines()
    {
        this.LeaderBoardLines.Clear();
        if(this.SelectedSession == null)
        {
            return;
        }

        var lines = DbRepository.GetLeaderBoardLines(this.SelectedSession.Id);

        foreach(var leaderBoardLine in lines)
        {
            this.LeaderBoardLines.Add(
                new LeaderBoardLineViewModel(leaderBoardLine, this.serverDetails));
        }
    }

    private void LoadPenalties()
    {
        this.Penalties.Clear();

        if(this.SelectedSession == null)
        {
            return;
        }

        var sessionPenalties = DbRepository.GetPenalties(this.SelectedSession.Id);

        foreach(var penalty in sessionPenalties)
        {
            this.Penalties.Add(new PenaltyViewModel(penalty, this.serverDetails));
        }
    }

    private async void LoadServerSessions()
    {
        if(this.serverDetails == null)
        {
            return;
        }

        var serverSyncDialog = new ServerSyncDialog
                               {
                                   Owner = Application.Current.MainWindow
                               };

        var serverSyncViewModel = new ServerSyncDialogViewModel(serverSyncDialog);
        serverSyncDialog.DataContext = serverSyncViewModel;
        serverSyncDialog.Show();
        await serverSyncViewModel.Start(this.serverDetails);

        this.Sessions.Clear();
        var sessions = DbRepository.GetSessionsForServer(this.serverDetails.Id);
        this.SynchronisationCompleted?.Invoke(this, EventArgs.Empty);

        foreach(var session in sessions)
        {
            this.Sessions.Add(session);
        }

        if(!this.Sessions.Any())
        {
            return;
        }

        this.SelectedSession = this.Sessions[0];
    }

    private void LoadSession()
    {
        this.LoadLeaderBoardLines();
        this.LoadLaps();
        this.LoadPenalties();
    }
}