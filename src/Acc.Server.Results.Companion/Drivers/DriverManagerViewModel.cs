using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Acc.Server.Results.Companion.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Acc.Server.Results.Companion.Drivers;

public class DriverManagerViewModel : ObservableObject
{
    internal event EventHandler DriversChanged;

    public DriverManagerViewModel()
    {
        this.AddDriver = new RelayCommand(this.HandleAddDriver);
        this.EditDriver = new RelayCommand<DriverViewModel>(this.HandleEditDriver);
        this.LoadDrivers();
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

    public ICommand AddDriver { get; }
    public ICommand EditDriver { get; }

    public ObservableCollection<DriverViewModel> Drivers { get; } = new();

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

    private void LoadDrivers()
    {
        this.Drivers.Clear();

        var drivers = DbRepository.GetDrivers();
        foreach(var driver in drivers)
        {
            this.Drivers.Add(new DriverViewModel(driver));
        }
    }

    public void Refresh()
    {
        this.LoadDrivers();
    }
}