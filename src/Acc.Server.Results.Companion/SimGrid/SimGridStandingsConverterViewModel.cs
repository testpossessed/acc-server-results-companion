using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using Acc.Server.Results.Companion.SimGrid.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace Acc.Server.Results.Companion.SimGrid;

public class SimGridStandingsConverterViewModel : ObservableObject
{
    private string selectedFilePath;

    public SimGridStandingsConverterViewModel()
    {
        this.SelectFilePath = new RelayCommand(this.HandleSelectFilePath);
    }

    public ObservableCollection<SimGridStandingsClass> CarClasses { get; } = new();

    public ICommand SelectFilePath { get; }

    public string SelectedFilePath
    {
        get => this.selectedFilePath;
        set => this.SetProperty(ref this.selectedFilePath, value);
    }

    private void HandleSelectFilePath()
    {
        var openFileDialog = new OpenFileDialog
                             {
                                 FilterIndex = 0,
                                 Filter = "SimGrid Standings File (*.json)|*.json",
                                 InitialDirectory =
                                     Environment.GetFolderPath(Environment.SpecialFolder
                                         .MyComputer),
                                 ShowReadOnly = true
                             };
        var dialogResult = openFileDialog.ShowDialog();
        if(dialogResult == DialogResult.Cancel)
        {
            return;
        }

        this.SelectedFilePath = openFileDialog.FileName;

        var json = File.ReadAllText(this.SelectedFilePath);
        var carClasses = JsonConvert.DeserializeObject<List<SimGridStandingsClass>>(json);

        this.CarClasses.Clear();
        foreach(var carClass in carClasses)
        {
            this.CarClasses.Add(carClass);
        }
    }
}