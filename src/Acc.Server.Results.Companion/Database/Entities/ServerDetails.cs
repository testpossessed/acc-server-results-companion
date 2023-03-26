using System;
using System.Collections.Generic;

namespace Acc.Server.Results.Companion.Database.Entities;

public class ServerDetails
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string FtpFolderPath { get; set; }
    public bool IsLocalFolder { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public ICollection<Session> Sessions { get; set; }
    public string Username { get; set; }
}