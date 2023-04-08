using System;
using System.Collections.ObjectModel;
using Acc.Server.Results.Companion.Core.Models;
using Acc.Server.Results.Companion.Database;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Acc.Server.Results.Companion.Reporting.FastestLaps;

internal class OverallFastestLapsViewModel : ObservableObject
{
    private string filterMode;
    private bool includeInvalidLaps;
    private ServerListItem selectedServer;

    public OverallFastestLapsViewModel()
    {
        this.FilterMode = "All";
        this.IncludeInvalidLaps = false;
        this.LoadServers();
    }

    public ObservableCollection<FastestLapViewModel> FastestLaps { get; } = new();
    public ObservableCollection<ServerListItem> Servers { get; } = new();

    public string FilterMode
    {
        get => this.filterMode;
        set
        {
            this.SetProperty(ref this.filterMode, value);
            this.LoadFastestLaps();
        }
    }

    public bool IncludeInvalidLaps
    {
        get => this.includeInvalidLaps;
        set
        {
            this.SetProperty(ref this.includeInvalidLaps, value);
            this.LoadFastestLaps();
        }
    }

    public ServerListItem SelectedServer
    {
        get => this.selectedServer;
        set
        {
            this.SetProperty(ref this.selectedServer, value);
            this.LoadFastestLaps();
        }
    }

    private void LoadFastestLaps()
    {
        this.FastestLaps.Clear();

        if(this.SelectedServer == null)
        {
            return;
        }

        var fastestLapsForServer = DbRepository.GetOverallFastestLaps(this.SelectedServer.Id, this.FilterMode, this.IncludeInvalidLaps);

        foreach(var lapViewModel in fastestLapsForServer)
        {
            this.FastestLaps.Add(lapViewModel);
        }
    }

    private void LoadServers()
    {
        this.Servers.Clear();

        this.Servers.Add(new ServerListItem
                         {
                             Id = 0,
                             Name = "All"
                         });

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