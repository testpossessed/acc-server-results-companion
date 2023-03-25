using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Acc.Server.Results.Companion.Database.Entities;

namespace Acc.Server.Results.Companion.Sessions;

public partial class LeaderBoard : UserControl
{
    public static readonly DependencyProperty LeaderBoardLinesProperty =
        DependencyProperty.Register(nameof(LeaderBoardLines),
            typeof(ObservableCollection<LeaderBoardLine>),
            typeof(LeaderBoard),
            new PropertyMetadata(default(ObservableCollection<LeaderBoardLine>)));

    public LeaderBoard()
    {
        this.InitializeComponent();
    }

    public ObservableCollection<LeaderBoardLine> LeaderBoardLines
    {
        get => (ObservableCollection<LeaderBoardLine>)this.GetValue(LeaderBoardLinesProperty);
        set
        {
            this.SetValue(LeaderBoardLinesProperty, value);
        }
    }
}