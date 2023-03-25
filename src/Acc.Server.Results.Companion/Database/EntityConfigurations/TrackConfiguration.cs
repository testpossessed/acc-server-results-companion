using System;
using Acc.Server.Results.Companion.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acc.Server.Results.Companion.Database.EntityConfigurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasData(new Track
                        {
                            Id = 1,
                            AccTrackId = "barcelona",
                            Corners = 16,
                            Name = "Barcelona",
                            TrackLength = 4655
                        },
            new Track
            {
                Id = 2,
                AccTrackId = "brands_hatch",
                Corners = 9,
                Name = "Brands Hatch",
                TrackLength = 3908
            },
            new Track
            {
                Id = 3,
                AccTrackId = "cota",
                Corners = 20,
                Name = "Circuit of the Americas",
                TrackLength = 5513
            },
            new Track
            {
                Id = 4,
                AccTrackId = "donington",
                Corners = 12,
                Name = "Donington Park",
                TrackLength = 4020
            },
            new Track
            {
                Id = 5,
                AccTrackId = "hungaroring",
                Corners = 14,
                Name = "Hungaroring",
                TrackLength = 4381
            },
            new Track
            {
                Id = 6,
                AccTrackId = "imola",
                Corners = 22,
                Name = "Imola",
                TrackLength = 4959
            },
            new Track
            {
                Id = 7,
                AccTrackId = "indianapolis",
                Corners = 14,
                Name = "Indianapolis",
                TrackLength = 4167
            },
            new Track
            {
                Id = 8,
                AccTrackId = "kyalami",
                Corners = 16,
                Name = "Kyalami",
                TrackLength = 4522
            },
            new Track
            {
                Id = 9,
                AccTrackId = "laguna_seca",
                Corners = 11,
                Name = "Laguna Seca",
                TrackLength = 33602
            },
            new Track
            {
                Id = 10,
                AccTrackId = "misano",
                Corners = 16,
                Name = "Misano",
                TrackLength = 4226
            },
            new Track
            {
                Id = 11,
                AccTrackId = "monza",
                Corners = 11,
                Name = "Monza",
                TrackLength = 5793
            },
            new Track
            {
                Id = 12,
                AccTrackId = "mount_panorama",
                Corners = 23,
                Name = "Mount Panorama",
                TrackLength = 6213
            },
            new Track
            {
                Id = 13,
                AccTrackId = "nurburgring",
                Corners = 17,
                Name = "Nurburgring",
                TrackLength = 5173
            },
            new Track
            {
                Id = 14,
                AccTrackId = "oulton_park",
                Corners = 17,
                Name = "Oulton Park",
                TrackLength = 4307
            },
            new Track
            {
                Id = 15,
                AccTrackId = "paul_ricard",
                Corners = 13,
                Name = "Paul Ricard",
                TrackLength = 5770
            },
            new Track
            {
                Id = 16,
                AccTrackId = "silverstone",
                Corners = 18,
                Name = "Silverstone",
                TrackLength = 5891
            },
            new Track
            {
                Id = 17,
                AccTrackId = "snetterton",
                Corners = 12,
                Name = "Snetterton",
                TrackLength = 4779
            },
            new Track
            {
                Id = 18,
                AccTrackId = "spa",
                Corners = 19,
                Name = "Spa Francorchamps",
                TrackLength = 7004
            },
            new Track
            {
                Id = 19,
                AccTrackId = "suzuka",
                Corners = 18,
                Name = "Suzuka",
                TrackLength = 5807
            },
            new Track
            {
                Id = 20,
                AccTrackId = "watkins_glen",
                Corners = 11,
                Name = "Watkins Glen",
                TrackLength = 5552
            },
            new Track
            {
                Id = 21,
                AccTrackId = "zandvoort",
                Corners = 14,
                Name = "Zandvoort",
                TrackLength = 4252
            },
            new Track
            {
                Id = 22,
                AccTrackId = "zolder",
                Corners = 10,
                Name = "Zolder",
                TrackLength = 4011
            });
    }
}