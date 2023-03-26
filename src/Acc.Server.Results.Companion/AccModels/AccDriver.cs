using System;

namespace Acc.Server.Results.Companion.AccModels;

public class AccDriver
{
    public string DisplayName => $"{(string.IsNullOrWhiteSpace(this.FirstName)? "**": this.FirstName[..1])}. {(string.IsNullOrWhiteSpace(this.LastName)? "**": this.LastName)}";
    public string FullName =>
        $"{(string.IsNullOrWhiteSpace(this.FirstName)? "**": this.FirstName)} {(string.IsNullOrWhiteSpace(this.LastName)? "**": this.LastName)}";
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PlayerId { get; set; }
    public string ShortName { get; set; }
}