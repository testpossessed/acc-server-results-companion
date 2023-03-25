using System;
using Acc.Server.Results.Companion.Core.Services;
using Acc.Server.Results.Companion.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Acc.Server.Results.Companion.Database
{
    internal class AppDbContext : DbContext
    {
        public DbSet<ServerDetails> Servers { get; set; } 
        public DbSet<Session> Sessions { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                $"DataSource={PathProvider.AppDataFolderPath}\\AccServerResultsCompanion.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType()
                                                             .Assembly);
        }
    }
}