using System;
using Acc.Server.Results.Companion.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acc.Server.Results.Companion.Database.EntityConfigurations;

internal class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasMany(x => x.LeaderBoardLines)
               .WithOne()
               .HasForeignKey(x => x.SessionId);

        builder.HasMany(x => x.Laps)
               .WithOne()
               .HasForeignKey(x => x.SessionId);

        builder.HasMany(x => x.Penalties)
               .WithOne()
               .HasForeignKey(x => x.SessionId);
    }
}