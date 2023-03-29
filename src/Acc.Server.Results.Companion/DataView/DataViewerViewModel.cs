using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Acc.Server.Results.Companion.Database;
using Acc.Server.Results.Companion.Database.Entities;
using Acc.Server.Results.Companion.Server.Sync;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Acc.Server.Results.Companion.DataView;

public class DataViewerViewModel : ObservableObject
{
    private Session selectedSession;
    private ServerDetails serverDetails;

    public ObservableCollection<Lap> Laps { get; } = new();
    public ObservableCollection<LeaderBoardLine> LeaderBoardLines { get; } = new();
    public ObservableCollection<Penalty> Penalties { get; } = new();
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
            this.Laps.Add(lap);
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
            this.LeaderBoardLines.Add(leaderBoardLine);
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
            this.Penalties.Add(penalty);
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

    public void Refresh()
    {
        this.LoadSession();
    }
}