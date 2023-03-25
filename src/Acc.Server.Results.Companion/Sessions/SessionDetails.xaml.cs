using System;
using System.Windows;
using System.Windows.Controls;
using Acc.Server.Results.Companion.Database.Entities;

namespace Acc.Server.Results.Companion.Sessions;

public partial class SessionDetails : UserControl
{
    public static readonly DependencyProperty SessionProperty = DependencyProperty.Register(
        nameof(Session),
        typeof(Session),
        typeof(SessionDetails),
        new PropertyMetadata(default(Session)));

    public SessionDetails()
    {
        this.InitializeComponent();
    }

    public Session Session
    {
        get => (Session)this.GetValue(SessionProperty);
        set => this.SetValue(SessionProperty, value);
    }
}