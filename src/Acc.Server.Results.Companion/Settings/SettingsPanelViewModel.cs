using System;
using System.Windows;
using System.Windows.Input;
using Acc.Server.Results.Companion.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Acc.Server.Results.Companion.Settings;

public class SettingsPanelViewModel : ObservableObject
{
    private string flagIconUrlBase;
    private Visibility settingsVisibility;

    private string vehicleIconUrlBase;

    public SettingsPanelViewModel()
    {
        this.ToggleVisibility = new RelayCommand(this.HandleToggleVisibility);
        this.SettingsVisibility = Visibility.Collapsed;

        this.SyncSettings();
    }

    public ICommand ToggleVisibility { get; }

    public string FlagIconUrlBase
    {
        get => this.flagIconUrlBase;
        set
        {
            this.SetProperty(ref this.flagIconUrlBase, value);
            UserSettingsProvider.SetFlagIconUrlBase(value);
        }
    }

    public Visibility SettingsVisibility
    {
        get => this.settingsVisibility;
        set => this.SetProperty(ref this.settingsVisibility, value);
    }

    public string VehicleIconUrlBase
    {
        get => this.vehicleIconUrlBase;
        set
        {
            this.SetProperty(ref this.vehicleIconUrlBase, value);
            UserSettingsProvider.SetVehicleIconUrlBase(value);
        }
    }

    private void HandleToggleVisibility()
    {
        this.SettingsVisibility = this.SettingsVisibility == Visibility.Visible
                                      ? Visibility.Collapsed
                                      : Visibility.Visible;
    }

    private void SyncSettings()
    {
        var settings = UserSettingsProvider.GetSettings();
        this.FlagIconUrlBase = settings.FlagIconUrlBase;
        this.VehicleIconUrlBase = settings.VehicleIconUrlBase;
    }
}