using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Acc.Server.Results.Companion.Database.Entities;

namespace Acc.Server.Results.Companion.Sessions;

public partial class SessionLaps : UserControl
{
    public static readonly DependencyProperty LapsProperty = DependencyProperty.Register(
        nameof(Laps),
        typeof(ObservableCollection<Lap>),
        typeof(SessionLaps),
        new PropertyMetadata(default(ObservableCollection<Lap>)));

    public SessionLaps()
    {
        this.InitializeComponent();
    }

    public ObservableCollection<Lap> Laps
    {
        get => (ObservableCollection<Lap>)this.GetValue(LapsProperty);
        set
        {
            this.SetValue(LapsProperty, value);
        }
    }
}