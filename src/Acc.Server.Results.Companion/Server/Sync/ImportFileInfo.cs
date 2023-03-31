using System;
using System.IO;
using Acc.Server.Results.Companion.Core;

namespace Acc.Server.Results.Companion.Server.Sync;

internal class ImportFileInfo
{
    public ImportFileInfo(string filePath)
    {
        this.FilePath = filePath;
        this.SortIndex = this.GetSortIndex(filePath);
        this.TimeStamp = this.GetTimestampFromFileName(filePath);
        this.IsEntryList = filePath.Contains(Constants.EntryListKey);
    }

    public string FilePath { get; set; }

    public bool IsEntryList { get; set; }

    public int SortIndex { get; set; }

    public DateTime TimeStamp { get; set; }

    private int GetSortIndex(string filePath)
    {
        if(filePath.Contains("_FP"))
        {
            return 1;
        }

        if(filePath.Contains("_Q"))
        {
            return 2;
        }

        return filePath.Contains("_R")? 3: 0;
    }

    private DateTime GetTimestampFromFileName(string filePath)
    {
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var elements = fileName.Split('_', StringSplitOptions.RemoveEmptyEntries);

        var year = elements[0][..2];
        var month = elements[0]
            .Substring(2, 2);
        var day = elements[0]
            .Substring(4, 2);

        var hour = elements[1][..2];
        var minute = elements[1]
            .Substring(2, 2);
        var second = elements[1]
            .Substring(4, 2);

        return new DateTime(int.Parse(year) + 2000,
            int.Parse(month),
            int.Parse(day),
            int.Parse(hour),
            int.Parse(minute),
            int.Parse(second));
    }
}