using System;
using System.Windows;
using System.Windows.Controls;

namespace Acc.Server.Results.Companion.Server.Stats;

public partial class ServerStatItem : UserControl
{
    public static readonly DependencyProperty StatLabelProperty = DependencyProperty.Register(
        nameof(StatLabel),
        typeof(string),
        typeof(ServerStatItem),
        new PropertyMetadata(default(string)));

    public static readonly DependencyProperty StatValueProperty = DependencyProperty.Register(
        nameof(StatValue),
        typeof(string),
        typeof(ServerStatItem),
        new PropertyMetadata(default(string)));

    public ServerStatItem()
    {
        this.InitializeComponent();
    }

    public string StatLabel
    {
        get => (string)this.GetValue(StatLabelProperty);
        set => this.SetValue(StatLabelProperty, value);
    }

    public string StatValue
    {
        get => (string)this.GetValue(StatValueProperty);
        set => this.SetValue(StatValueProperty, value);
    }
}