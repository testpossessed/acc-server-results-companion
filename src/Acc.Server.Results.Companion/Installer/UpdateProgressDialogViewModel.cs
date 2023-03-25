using CommunityToolkit.Mvvm.ComponentModel;
using Squirrel;
using System;
using System.Linq;
using System.Windows;
using Acc.Server.Results.Companion.Core.Services;

namespace Acc.Server.Results.Companion.Installer;

public class UpdateProgressDialogViewModel : ObservableObject
{
    private string installedVersion;
    private string newVersion; 
    private int progress;
    private readonly UpdateInfo updateInfo;
    private readonly UpdateManager updateManager;
    private readonly Window window;

    public UpdateProgressDialogViewModel(Window window,
        UpdateManager updateManager,
        UpdateInfo updateInfo)
    {
        this.window = window;
        this.updateManager = updateManager;
        this.updateInfo = updateInfo;
    }

    public string InstalledVersion
    {
        get => this.installedVersion;
        set => this.SetProperty(ref this.installedVersion, value);
    }

    public string NewVersion
    {
        get => this.newVersion;
        set => this.SetProperty(ref this.newVersion, value);
    }

    public int Progress
    {
        get => this.progress;
        set => this.SetProperty(ref this.progress, value);
    }

    internal async void Update()
    {
        LogWriter.LogDebug("Detected new version/s of ACC Server Results Companion to install.");

        this.InstalledVersion = this.updateInfo.LatestLocalReleaseEntry.Version.ToString();
        LogWriter.LogDebug($"Installed Version: {this.InstalledVersion}");

        this.NewVersion = string.Join(",",
            this.updateInfo.ReleasesToApply.Select(e => e.Version.ToString())
                .ToArray());
        LogWriter.LogDebug($"New Versions: {this.NewVersion}");

        try
        {
            await this.updateManager.UpdateApp(p => this.Progress = p);
            this.updateManager.Dispose();
            UpdateManager.RestartApp();
        }
        catch(Exception exception)
        {
            LogWriter.LogError(exception, "Error during update");
            MessageBox.Show(
                "An unexpected error happened during the update, please let us know via GitHub Issues at https://github.com/testpossessed/acc-server-results-companion/issues and we will try to get you up and running again ASAP",
                "Update Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
        finally
        {
            this.updateManager.Dispose();
        }
    }
}