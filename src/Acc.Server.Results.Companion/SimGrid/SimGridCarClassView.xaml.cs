using System;
using System.Windows;
using System.Windows.Controls;
using Acc.Server.Results.Companion.SimGrid.Models;
using Microsoft.Win32;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;

namespace Acc.Server.Results.Companion.SimGrid;

public partial class SimGridCarClassView : UserControl
{
    public static readonly DependencyProperty CarClassProperty = DependencyProperty.Register(
        nameof(CarClass),
        typeof(SimGridStandingsClass),
        typeof(SimGridCarClassView),
        new PropertyMetadata(default(SimGridStandingsClass)));

    public SimGridCarClassView()
    {
        this.InitializeComponent();
    }

    public SimGridStandingsClass CarClass
    {
        get => (SimGridStandingsClass)this.GetValue(CarClassProperty);
        set => this.SetValue(CarClassProperty, value);
    }

    private void HandleExportToExcel(object sender, RoutedEventArgs eventArgs)
    {
        var options = new ExcelExportingOptions
                      {
                          ExcelVersion = ExcelVersion.Excel2016,
                          ExportAllPages = true
                      };
        var excelEngine = this.DataGrid.ExportToExcel(this.DataGrid.View, options);
        var workBook = excelEngine.Excel.Workbooks[0];

        var sfd = new SaveFileDialog
                  {
                      FilterIndex = 2,
                      Filter =
                          "Excel 97 to 2003 Files(*.xls)|*.xls|Excel 2007 to 2010 Files(*.xlsx)|*.xlsx|Excel 2013 File(*.xlsx)|*.xlsx"
                  };

        if(sfd.ShowDialog() != true)
        {
            return;
        }

        using(var stream = sfd.OpenFile())
        {
            if(sfd.FilterIndex == 1)
            {
                workBook.Version = ExcelVersion.Excel97to2003;
            }

            else if(sfd.FilterIndex == 2)
            {
                workBook.Version = ExcelVersion.Excel2010;
            }

            else
            {
                workBook.Version = ExcelVersion.Excel2013;
            }

            workBook.SaveAs(stream);
        }

        MessageBox.Show("The data has been exported to Excel",
            "Export To Excel",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
}