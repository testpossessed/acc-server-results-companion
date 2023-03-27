using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Acc.Server.Results.Companion.Database.Entities;

namespace Acc.Server.Results.Companion.Sessions;

public partial class SessionPenalties : UserControl
{
    public static readonly DependencyProperty PenaltiesProperty = DependencyProperty.Register(
        nameof(Penalties),
        typeof(ObservableCollection<Penalty>),
        typeof(SessionPenalties),
        new PropertyMetadata(default(ObservableCollection<Penalty>)));

    public SessionPenalties()
    {
        this.InitializeComponent();
    }

    public ObservableCollection<Penalty> Penalties
    {
        get => (ObservableCollection<Penalty>)this.GetValue(PenaltiesProperty);
        set => this.SetValue(PenaltiesProperty, value);
    }
}