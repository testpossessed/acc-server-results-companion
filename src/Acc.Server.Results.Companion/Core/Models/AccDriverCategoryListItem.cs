using System;
using Acc.Server.Results.Companion.AccEnums;

namespace Acc.Server.Results.Companion.Core.Models;

public class AccDriverCategoryListItem
{
    public AccDriverCategoryListItem(AccDriverCategory category)
    {
        this.Category = category;
    }

    public AccDriverCategory Category { get; }
    public string DisplayText => this.Category.ToString();
}