using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Acc.Server.Results.Companion.AccEnums;
using Acc.Server.Results.Companion.Core.Models;
using Acc.Server.Results.Companion.Database;
using Acc.Server.Results.Companion.Database.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Acc.Server.Results.Companion.Drivers;

public class DriverEditorViewModel : ObservableObject
{
    private readonly DriverEditor driverEditor;
    private Driver existingDriver;
    private string firstName;
    private string firstNameOverride;
    private bool isExistingDriver;
    private bool isImportedDriver;
    private string lastName;
    private string lastNameOverride;
    private string ourCategory;
    private AccDriverCategoryListItem selectedAccCategory;
    private AccNationalityListItem selectedNationality;
    private AccNationalityListItem selectedNationalityOverride;
    private string shortName;
    private string steamId;

    public DriverEditorViewModel(DriverEditor driverEditor)
    {
        this.driverEditor = driverEditor;
        this.Cancel = new RelayCommand(this.HandleCancel);
        this.Save = new RelayCommand(this.HandleSave, this.CanExecuteSave);
        this.PopulateListItems();
        this.PropertyChanged += this.HandlePropertyChanged;
    }

    public ObservableCollection<AccDriverCategoryListItem> AccCategories { get; } = new();
    public ICommand Cancel { get; }
    public ObservableCollection<AccNationalityListItem> Nationalities { get; } = new();
    public ICommand Save { get; }

    public string FirstName
    {
        get => this.firstName;
        set => this.SetProperty(ref this.firstName, value);
    }

    public string FirstNameOverride
    {
        get => this.firstNameOverride;
        set => this.SetProperty(ref this.firstNameOverride, value);
    }

    public bool IsExistingDriver
    {
        get => this.isExistingDriver;
        set => this.SetProperty(ref this.isExistingDriver, value);
    }

    public bool IsImportedDriver
    {
        get => this.isImportedDriver;
        set => this.SetProperty(ref this.isImportedDriver, value);
    }

    public string LastName
    {
        get => this.lastName;
        set
        {
            this.SetProperty(ref this.lastName, value);
            if(value.Length < 3)
            {
                return;
            }
            if(!this.IsExistingDriver && string.IsNullOrWhiteSpace(this.ShortName))
            {
                this.ShortName = value[..3]
                                      .ToUpperInvariant();
            } 
        }
    }

    public string LastNameOverride
    {
        get => this.lastNameOverride;
        set => this.SetProperty(ref this.lastNameOverride, value);
    }

    public string OurCategory
    {
        get => this.ourCategory;
        set => this.SetProperty(ref this.ourCategory, value);
    }

    public AccDriverCategoryListItem SelectedAccCategory
    {
        get => this.selectedAccCategory;
        set => this.SetProperty(ref this.selectedAccCategory, value);
    }

    public AccNationalityListItem SelectedNationality
    {
        get => this.selectedNationality;
        set => this.SetProperty(ref this.selectedNationality, value);
    }

    public AccNationalityListItem SelectedNationalityOverride
    {
        get => this.selectedNationalityOverride;
        set => this.SetProperty(ref this.selectedNationalityOverride, value);
    }

    public string ShortName
    {
        get => this.shortName;
        set => this.SetProperty(ref this.shortName, value);
    }

    public string SteamId
    {
        get => this.steamId;
        set => this.SetProperty(ref this.steamId, value);
    }

    internal void SetExistingDriver(Driver driver)
    {
        this.existingDriver = driver;
        this.IsExistingDriver = this.existingDriver != null;
        this.IsImportedDriver = this.existingDriver?.IsImported is true;
        this.SteamId = driver.Id;
        this.FirstName = driver.FirstName;
        this.LastName = driver.LastName;
        this.ShortName = driver.ShortName;
        this.SelectedAccCategory =
            this.AccCategories.FirstOrDefault(
                c => c.Category == (AccDriverCategory)driver.DriverCategoryCode);
        this.OurCategory = driver.OurCategory;
        this.SelectedNationality =
            this.Nationalities.FirstOrDefault(
                c => c.Nationality == (AccNationality)driver.NationalityCode);
        if(driver.NationalityCodeOverride.HasValue)
        {
            this.SelectedNationalityOverride =
                this.Nationalities.FirstOrDefault(
                    c => c.Nationality == (AccNationality)driver.NationalityCodeOverride);
        }
        this.FirstNameOverride = driver.FirstNameOverride;
        this.LastNameOverride = driver.LastNameOverride;
    }

    private bool CanExecuteSave()
    {
        if(this.IsExistingDriver)
        {
            return true; 
        }

        return !string.IsNullOrWhiteSpace(this.SteamId)
               && !string.IsNullOrWhiteSpace(this.FirstName)
               && !string.IsNullOrWhiteSpace(this.LastName)
               && this.SelectedNationality?.Nationality != AccNationality.Any
               && this.SteamId.StartsWith("S") && this.SteamId.Length == 18;
    }

    private void HandleCancel()
    {
        this.driverEditor.DialogResult = false;
        this.driverEditor.Close();
    }

    private void HandlePropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
    {
        ((RelayCommand)this.Save).NotifyCanExecuteChanged();
    }

    private void HandleSave()
    {
        if(!this.IsExistingDriver && DbRepository.DriverExists(this.SteamId))
        {
            MessageBox.Show(Application.Current.MainWindow!,
                $"A Driver with Steam ID {this.SteamId} already exists, it may have been imported automatically from the server",
                "Existing Driver",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        if(this.IsExistingDriver)
        {
            if(!this.existingDriver.IsImported)
            {
                this.existingDriver.FirstName = this.FirstName;
                this.existingDriver.LastName = this.LastName;
                this.existingDriver.DriverCategory = this.SelectedAccCategory.DisplayText;
                this.existingDriver.DriverCategoryCode = (int)this.SelectedAccCategory.Category;
                this.existingDriver.Nationality = this.SelectedNationality.DisplayText;
                this.existingDriver.NationalityCode = (int)this.SelectedNationality.Nationality;
                this.existingDriver.ShortName = this.ShortName;
            }

            this.existingDriver.FirstNameOverride = this.FirstNameOverride;
            this.existingDriver.LastNameOverride = this.LastNameOverride;
            this.existingDriver.OurCategory = this.OurCategory;
            if(this.SelectedNationalityOverride != null)
            {
                this.existingDriver.NationalityCodeOverride =
                    (int?)this.SelectedNationalityOverride.Nationality;
                this.existingDriver.NationalityOverride =
                    this.SelectedNationalityOverride.DisplayText;
            }

            DbRepository.UpdateDriver(this.existingDriver);
        }
        else
        {

            var driver = new Driver
                         {
                             Id = this.SteamId,
                             FirstName = this.FirstName,
                             LastName = this.LastName,
                             ShortName = this.ShortName,
                             DriverCategoryCode = (int)this.SelectedAccCategory.Category,
                             DriverCategory = this.SelectedAccCategory.DisplayText,
                             NationalityCode = (int)this.SelectedNationality.Nationality,
                             Nationality = this.SelectedNationality.DisplayText,
                             IsImported = false,
                             OurCategory = this.ourCategory
                         };
            DbRepository.AddDriver(driver);
        }

        this.driverEditor.DialogResult = true;
        this.driverEditor.Close();
    }

    private void PopulateListItems()
    {
        this.AccCategories.Add(new AccDriverCategoryListItem(AccDriverCategory.Bronze));
        this.AccCategories.Add(new AccDriverCategoryListItem(AccDriverCategory.Silver));
        this.AccCategories.Add(new AccDriverCategoryListItem(AccDriverCategory.Gold));
        this.AccCategories.Add(new AccDriverCategoryListItem(AccDriverCategory.Platinum));
        this.SelectedAccCategory = this.AccCategories[0];

        foreach(var nationality in (AccNationality[])Enum.GetValues<AccNationality>())
        {
            this.Nationalities.Add(new AccNationalityListItem(nationality));
        }
    }
}