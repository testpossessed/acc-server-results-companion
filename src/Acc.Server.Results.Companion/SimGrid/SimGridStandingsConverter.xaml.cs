using System;
using Syncfusion.SfSkinManager;
using Syncfusion.Windows.Shared;

namespace Acc.Server.Results.Companion.SimGrid;

public partial class SimGridStandingsConverter : ChromelessWindow
{
    public SimGridStandingsConverter()
    {
        SfSkinManager.ApplyStylesOnApplication = true;
        this.InitializeComponent();
    }
}