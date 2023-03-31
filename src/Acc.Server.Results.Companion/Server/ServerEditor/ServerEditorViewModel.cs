using System;
using System.Windows.Forms;
using System.Windows.Input;
using Acc.Server.Results.Companion.Core;
using Acc.Server.Results.Companion.Core.Services;
using Acc.Server.Results.Companion.Database;
using Acc.Server.Results.Companion.Database.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Acc.Server.Results.Companion.Server.ServerEditor;

internal class ServerEditorViewModel : ObservableObject
{
    
    private readonly ServerEditor serverEditor;
    private string bronzeClassification;
    private ServerDetails existingServer;
    private string ftpFolderPath;
    private string goldClassification;
    private string hostName;
    private int hostPort;
    private string localFolderPath;
    private string name;
    private string password;
    private string platinumClassification;
    private string serverType;
    private string silverClassification;
    private string username;

    public ServerEditorViewModel(ServerEditor serverEditor)
    {
        this.serverEditor = serverEditor;
        this.Cancel = new RelayCommand(this.HandleCancel);
        this.Save = new RelayCommand(this.HandleSave, this.CanExecuteSave);
        this.SelectFolder = new RelayCommand(this.HandleSelectFolder);
        this.ServerType = Constants.FtpServerType;
        this.FtpFolderPath = "/";
        this.HostPort = 21;
        this.BronzeClassification = Constants.DriverClassAm;
        this.SilverClassification = Constants.DriverClassProAm;
        this.GoldClassification = Constants.DriverClassPro;
        this.PlatinumClassification = Constants.DriverClassPro;
    }

    public ICommand Cancel { get; }

    public ICommand Save { get; }

    public ICommand SelectFolder { get; }

    public string BronzeClassification
    {
        get => this.bronzeClassification;
        set => this.SetProperty(ref this.bronzeClassification, value);
    }

    public string FtpFolderPath
    {
        get => this.ftpFolderPath;
        set => this.SetProperty(ref this.ftpFolderPath, value);
    }

    public string GoldClassification
    {
        get => this.goldClassification;
        set => this.SetProperty(ref this.goldClassification, value);
    }

    public string HostName
    {
        get => this.hostName;
        set
        {
            this.SetProperty(ref this.hostName, value);
            this.NotifyCanExecuteSaveChanged();
        }
    }

    public int HostPort
    {
        get => this.hostPort;
        set
        {
            this.SetProperty(ref this.hostPort, value);
            this.NotifyCanExecuteSaveChanged();
        }
    }

    public string LocalFolderPath
    {
        get => this.localFolderPath;
        set
        {
            this.SetProperty(ref this.localFolderPath, value);
            this.NotifyCanExecuteSaveChanged();
        }
    }

    public string Name
    {
        get => this.name;
        set
        {
            this.SetProperty(ref this.name, value);
            this.NotifyCanExecuteSaveChanged();
        }
    }

    public string Password
    {
        get => this.password;
        set => this.SetProperty(ref this.password, value);
    }

    public string PlatinumClassification
    {
        get => this.platinumClassification;
        set => this.SetProperty(ref this.platinumClassification, value);
    }

    public string ServerType
    {
        get => this.serverType;
        set => this.SetProperty(ref this.serverType, value);
    }

    public string SilverClassification
    {
        get => this.silverClassification;
        set => this.SetProperty(ref this.silverClassification, value);
    }

    public string Username
    {
        get => this.username;
        set => this.SetProperty(ref this.username, value);
    }

    internal void SetServer(ServerDetails server)
    {
        this.existingServer = server;
        this.BronzeClassification = server.BronzeClassification;
        this.FtpFolderPath = server.FtpFolderPath;
        this.GoldClassification = server.GoldClassification;
        this.Name = server.Name;
        this.Password = server.Password;
        this.PlatinumClassification = server.PlatinumClassification;
        this.SilverClassification = server.SilverClassification;
        this.Username = server.Username;

        if(server.IsLocalFolder)
        {
            this.LocalFolderPath = server.Address;
            this.ServerType = Constants.FolderServerType;
        }
        else
        {
            var url = new Uri(server.Address);
            this.FtpFolderPath = server.FtpFolderPath;
            this.HostName = url.Host;
            this.HostPort = url.Port;
            this.ServerType = Constants.FtpServerType;
        }
    }

    private bool CanExecuteSave()
    {
        return !string.IsNullOrWhiteSpace(this.Name)
               && (this.HasValidFtpSettings() || this.HasValidFolderSettings());
    }

    private string GetAddress()
    {
        return this.ServerType == Constants.FtpServerType
                   ? this.LocalFolderPath
                   : $"ftp://{this.HostName}:{this.HostPort}";
    }

    private string GetBronzeClassificationOrDefault()
    {
        return string.IsNullOrWhiteSpace(this.BronzeClassification)
                   ? Constants.DriverClassAm
                   : this.BronzeClassification;
    }

    private string GetFtpFolderPath()
    {
        if(!this.FtpFolderPath.StartsWith("/"))
        {
            this.FtpFolderPath = $"/{this.FtpFolderPath}";
        }

        return this.FtpFolderPath;
    }

    private string GetGoldClassificationOrDefault()
    {
        return string.IsNullOrWhiteSpace(this.GoldClassification)
                   ? Constants.DriverClassPro
                   : this.GoldClassification;
    }

    private string GetPlatinumClassificationOrDefault()
    {
        return string.IsNullOrWhiteSpace(this.PlatinumClassification)
                   ? Constants.DriverClassPro
                   : this.PlatinumClassification;
    }

    private string GetSilverClassificationOrDefault()
    {
        return string.IsNullOrWhiteSpace(this.SilverClassification)
                   ? Constants.DriverClassProAm
                   : this.SilverClassification;
    }

    private void HandleCancel()
    {
        this.serverEditor.DialogResult = false;
        this.serverEditor.Close();
    }

    private void HandleSave()
    {
        var serverDetails = this.existingServer ?? new ServerDetails();

        serverDetails.Name = this.Name;
        serverDetails.Address = this.GetAddress();
        serverDetails.Username = this.Username;
        serverDetails.Password = this.Password;
        serverDetails.IsLocalFolder = this.ServerType == Constants.FolderServerType;
        serverDetails.FtpFolderPath = this.GetFtpFolderPath();
        serverDetails.BronzeClassification = this.GetBronzeClassificationOrDefault();
        serverDetails.SilverClassification = this.GetSilverClassificationOrDefault();
        serverDetails.GoldClassification = this.GetGoldClassificationOrDefault();
        serverDetails.PlatinumClassification = this.GetPlatinumClassificationOrDefault();

        if(this.existingServer != null)
        {
            DbRepository.UpdateServerDetails(serverDetails);
        }
        else
        {
            DbRepository.AddServerDetails(serverDetails);
        }

        UserSettingsProvider.SetLastServerId(serverDetails.Id);
        this.serverEditor.DialogResult = true;
        this.serverEditor.Close();
    }

    private void HandleSelectFolder()
    {
        var selectFolderDialog = new FolderBrowserDialog();
        var dialogResult = selectFolderDialog.ShowDialog();
        if(dialogResult == DialogResult.OK)
        {
            this.LocalFolderPath = selectFolderDialog.SelectedPath;
        }
    }

    private bool HasValidFolderSettings()
    {
        return this.ServerType == Constants.FolderServerType
               && !string.IsNullOrWhiteSpace(this.LocalFolderPath);
    }

    private bool HasValidFtpSettings()
    {
        return this.ServerType == Constants.FtpServerType && (!string.IsNullOrWhiteSpace(this.HostName)
                                                    && this.HostPort > 0);
    }

    private void NotifyCanExecuteSaveChanged()
    {
        ((RelayCommand)this.Save).NotifyCanExecuteChanged();
    }
}