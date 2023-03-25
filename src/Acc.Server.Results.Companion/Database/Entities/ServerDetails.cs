using System;

namespace Acc.Server.Results.Companion.Database.Entities
{
    public class ServerDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsLocalFolder { get; set; }
    }
}