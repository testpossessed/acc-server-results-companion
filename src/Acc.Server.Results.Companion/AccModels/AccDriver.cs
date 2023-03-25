using System;

namespace Acc.Server.Results.Companion.AccModels;

public class AccDriver
{
    public string DisplayName => $"{this.FirstName[..1]}. {this.LastName}";
    public string FullName => $"{this.FirstName} {this.LastName}";
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PlayerId { get; set; }
    public string ShortName { get; set; }
}