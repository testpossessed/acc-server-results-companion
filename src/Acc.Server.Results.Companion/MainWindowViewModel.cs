using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Acc.Server.Results.Companion.Core.Services;
using Acc.Server.Results.Companion.Database;
using Acc.Server.Results.Companion.Database.Entities;
using Acc.Server.Results.Companion.DataView;
using Acc.Server.Results.Companion.Drivers;
using Acc.Server.Results.Companion.Server.ServerEditor;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Acc.Server.Results.Companion;

internal class MainWindowViewModel : ObservableObject
{
    private bool isInitialised;
    private ServerDetails selectedServer;

    public MainWindowViewModel()
    {
        this.AddServer = new RelayCommand(this.HandleAddServer);
        this.EditServer = new RelayCommand(this.HandleEditServer, this.CanExecuteEditServer);
        this.Refresh = new RelayCommand(this.HandleRefresh);
    }

    private void HandleRefresh()
    {
        this.DriverManagerViewModel.Refresh();
        this.DataViewerViewModel.Refresh();
    }

    public ICommand Refresh { get; }
    public ICommand AddServer { get; }
    public DataViewerViewModel DataViewerViewModel { get; } = new();
    // public EventManagerViewModel EventManagerViewModel { get; } = new();
    public DriverManagerViewModel DriverManagerViewModel { get; } = new();
    public ICommand EditServer { get; }
    public ObservableCollection<ServerDetails> Servers { get; } = new();

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
            this.DataViewerViewModel.SetServerDetails(this.SelectedServer);
            this.DriverManagerViewModel.SetServerDetails(this.SelectedServer);
            this.NotifyCanExecuteEditServerChanged();
            // this.EventManagerViewModel.SetServerDetails(this.SelectedServer);
        }
    }

    internal void Init()
    {
        this.LoadServers();
        this.DriverManagerViewModel.DriversChanged += this.HandleDriversChanged;
        this.DataViewerViewModel.SynchronisationCompleted += this.HandleSynchronisationCompleted;
    }

    private bool CanExecuteEditServer()
    {
        return this.SelectedServer != null;
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

    private void HandleDriversChanged(object sender, EventArgs eventArgs)
    {
        this.DataViewerViewModel.Refresh();
    }

    private void HandleEditServer()
    {
        var serverEditor = new ServerEditor()
                           {
                               Owner = Application.Current.MainWindow
                           };
        var viewModel = new ServerEditorViewModel(serverEditor);
        viewModel.SetServer(this.SelectedServer);

        serverEditor.DataContext = viewModel;
        var dialogResult = serverEditor.ShowDialog();
        if(dialogResult is false)
        {
            return;
        }

        this.LoadServers();
    }

    private void HandleSynchronisationCompleted(object sender, EventArgs eventArgs)
    {
        this.DriverManagerViewModel.Refresh();
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
        UserSettingsProvider.SetLastServerId(this.SelectedServer.Id);
    }

    private void NotifyCanExecuteEditServerChanged()
    {
        ((RelayCommand)this.EditServer).NotifyCanExecuteChanged();
    }
}