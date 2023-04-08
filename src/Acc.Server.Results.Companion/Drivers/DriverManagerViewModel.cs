using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Acc.Server.Results.Companion.Database;
using Acc.Server.Results.Companion.Database.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Acc.Server.Results.Companion.Drivers;

public class DriverManagerViewModel : ObservableObject
{
    private ServerDetails serverDetails;

    public DriverManagerViewModel()
    {
        this.AddDriver = new RelayCommand(this.HandleAddDriver);
        this.EditDriver = new RelayCommand<DriverViewModel>(this.HandleEditDriver);
    }

    internal event EventHandler DriversChanged;
    public ICommand AddDriver { get; }
    public ObservableCollection<DriverViewModel> Drivers { get; } = new();
    public ICommand EditDriver { get; }

    public void Refresh()
    {
        this.LoadDrivers();
    }

    internal void SetServerDetails(ServerDetails serverDetails)
    {
        this.serverDetails = serverDetails;
        this.LoadDrivers();
    }

    private void HandleAddDriver()
    {
        var window = new DriverEditor
                     {
                         Owner = Application.Current.MainWindow
                     };
        var viewModel = new DriverEditorViewModel(window);
        window.DataContext = viewModel;
        var dialogResult = window.ShowDialog();
        if(dialogResult != true)
        {
            return;
        }

        this.LoadDrivers();
        this.DriversChanged?.Invoke(this, EventArgs.Empty);
    }

    private void HandleEditDriver(DriverViewModel driver)
    {
        var window = new DriverEditor
                     {
                         Owner = Application.Current.MainWindow
                     };
        var viewModel = new DriverEditorViewModel(window);
        viewModel.SetExistingDriver(driver.Driver);
        window.DataContext = viewModel;
        var dialogResult = window.ShowDialog();
        if(dialogResult != true)
        {
            return;
        }

        this.LoadDrivers();
        this.DriversChanged?.Invoke(this, EventArgs.Empty);
    }

    private void LoadDrivers()
    {
        this.Drivers.Clear();

        if(this.serverDetails == null)
        {
            return;
        }
        var drivers = DbRepository.GetDrivers();
        foreach(var driver in drivers)
        {
            this.Drivers.Add(new DriverViewModel(driver, this.serverDetails));
        }
    }
}