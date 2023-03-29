using System;
using Acc.Server.Results.Companion.AccEnums;

namespace Acc.Server.Results.Companion.Core.Models;

public class AccNationalityListItem
{
    public AccNationality Nationality { get; }
    public string DisplayText => this.Nationality.ToFriendlyName();

    public AccNationalityListItem(AccNationality nationality)
    {
        this.Nationality = nationality;
    }
}