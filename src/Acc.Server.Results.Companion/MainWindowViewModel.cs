﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Acc.Server.Results.Companion.Core.Services;
using Acc.Server.Results.Companion.Database;
using Acc.Server.Results.Companion.Database.Entities;
using Acc.Server.Results.Companion.ServerManagement.ServerEditor;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Acc.Server.Results.Companion;

internal class MainWindowViewModel : ObservableObject
{
    private bool isInitialised;
    private ServerDetails selectedServer;
    private Session selectedSession;

    public MainWindowViewModel()
    {
        this.AddServer = new RelayCommand(this.HandleAddServer);

        this.LoadServers();
    }

    public ICommand AddServer { get; }
    public ObservableCollection<LeaderBoardLine> LeaderBoardLines { get; } = new();
    public ObservableCollection<ServerDetails> Servers { get; } = new();
    public ObservableCollection<Session> Sessions { get; } = new();

    public bool IsInitialised
    {
        get => this.isInitialised;
        set => this.SetProperty(ref this.isInitialised, value);
    }

    public ServerDetails SelectedServer
    {
        get => this.selectedServer;
        set
        {
            this.SetProperty(ref this.selectedServer, value);
            this.LoadServerSessions();
        }
    }

    public Session SelectedSession
    {
        get => this.selectedSession;
        set
        {
            this.SetProperty(ref this.selectedSession, value);
            this.LoadLeaderBoardLines();
        }
    }

    private void HandleAddServer()
    {
        var serverEditor = new ServerEditor()
                           {
                               Owner = Application.Current.MainWindow
                           };
        var viewModel = new ServerEditorViewModel(serverEditor);

        serverEditor.DataContext = viewModel;
        var dialogResult = serverEditor.ShowDialog();
        if(dialogResult is false)
        {
            return;
        }

        this.LoadServers();
    }

    private void LoadLeaderBoardLines()
    {
        this.LeaderBoardLines.Clear();
        var lines = DbRepository.GetLeaderBoardLines(this.SelectedSession.Id);

        foreach(var leaderBoardLine in lines)
        {
            this.LeaderBoardLines.Add(leaderBoardLine);
        }
    }

    private void LoadServers()
    {
        this.Servers.Clear();

        var servers = DbRepository.GetServers();
        
        this.IsInitialised = servers.Any();

        foreach(var server in servers)
        {
            this.Servers.Add(server);
        }

        if(!this.Servers.Any())
        {
            return;
        }

        var userSettings = UserSettingsProvider.GetSettings();
        var lastServer = this.Servers.FirstOrDefault(s => s.Id == userSettings.LastServerId);
        this.SelectedServer = lastServer ?? this.Servers[0];
    }

    private void LoadServerSessions()
    {
        UserSettingsProvider.SetLastServerId(this.SelectedServer.Id);

        ServerSynchroniser.Sync(this.selectedServer);

        this.Sessions.Clear();
        var sessions = DbRepository.GetSessionsForServer(this.SelectedServer.Id);

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
}