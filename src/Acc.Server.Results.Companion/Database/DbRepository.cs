using System;
using Acc.Server.Results.Companion.Database.Entities;

namespace Acc.Server.Results.Companion.Database
{
    internal static class DbRepository
    {
        public static void AddServerDetails(ServerDetails serverDetails)
        {
            var dbContext = GetDbContext();

            dbContext.Servers.Add(serverDetails);
            dbContext.SaveChanges();
        }

        private static AppDbContext GetDbContext()
        {
            return new AppDbContext();
        }
    }
}