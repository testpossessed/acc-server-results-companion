using System;
using Acc.Server.Results.Companion.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acc.Server.Results.Companion.Database.EntityConfigurations;

internal class ServerDetailsConfiguration : IEntityTypeConfiguration<ServerDetails>
{
    public void Configure(EntityTypeBuilder<ServerDetails> builder)
    {
        builder.HasMany(x => x.Sessions)
               .WithOne()
               .HasForeignKey(x => x.ServerId);

        builder.Property(e => e.BronzeClassification)
               .IsRequired()
               .HasDefaultValueSql("'AM'");

        builder.Property(e => e.SilverClassification)
               .IsRequired()
               .HasDefaultValueSql("'PRO-AM'");

        builder.Property(e => e.GoldClassification)
               .IsRequired()
               .HasDefaultValueSql("'PRO'");

        builder.Property(e => e.PlatinumClassification)
               .IsRequired()
               .HasDefaultValueSql("'PRO'");
    }
}