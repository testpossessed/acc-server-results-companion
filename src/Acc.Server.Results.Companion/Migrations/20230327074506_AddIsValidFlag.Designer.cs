﻿// <auto-generated />
using System;
using Acc.Server.Results.Companion.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Acc.Server.Results.Companion.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230327074506_AddIsValidFlag")]
    partial class AddIsValidFlag
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Acc.Server.Results.Companion.Database.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccModelId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccModelName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Class")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxFuel")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxRpm")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccModelId = 0,
                            AccModelName = "porsche_991_gt3_r",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 9250,
                            Name = "Porsche 991 GT3 R",
                            Year = 2018
                        },
                        new
                        {
                            Id = 2,
                            AccModelId = 1,
                            AccModelName = "mercedes_amg_gt3",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 7900,
                            Name = "Mercedes-AMG GT3",
                            Year = 2015
                        },
                        new
                        {
                            Id = 3,
                            AccModelId = 2,
                            AccModelName = "ferrari_488_gt3",
                            Class = "GT3",
                            MaxFuel = 110,
                            MaxRpm = 7300,
                            Name = "Ferrari 488 GT3",
                            Year = 2018
                        },
                        new
                        {
                            Id = 4,
                            AccModelId = 3,
                            AccModelName = "audi_r8_lms",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 8650,
                            Name = "Audi R8 LMS",
                            Year = 2015
                        },
                        new
                        {
                            Id = 5,
                            AccModelId = 4,
                            AccModelName = "lamborghini_huracán_gt3",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 8650,
                            Name = "Lamborghini Huracán GT3",
                            Year = 2015
                        },
                        new
                        {
                            Id = 6,
                            AccModelId = 5,
                            AccModelName = "mclaren_650s_gt3",
                            Class = "GT3",
                            MaxFuel = 125,
                            MaxRpm = 7500,
                            Name = "McLaren 650S GT3",
                            Year = 2015
                        },
                        new
                        {
                            Id = 7,
                            AccModelId = 6,
                            AccModelName = "nissan_gt_r_nismo_gt3",
                            Class = "GT3",
                            MaxFuel = 132,
                            MaxRpm = 7500,
                            Name = "Nissan GT-R Nismo GT3",
                            Year = 2018
                        },
                        new
                        {
                            Id = 8,
                            AccModelId = 7,
                            AccModelName = "bmw_m6_gt3",
                            Class = "GT3",
                            MaxFuel = 125,
                            MaxRpm = 7100,
                            Name = "BMW M6 GT3",
                            Year = 2017
                        },
                        new
                        {
                            Id = 9,
                            AccModelId = 8,
                            AccModelName = "bentley_continental_gt3_2018",
                            Class = "GT3",
                            MaxFuel = 132,
                            MaxRpm = 7400,
                            Name = "Bentley Continental GT3",
                            Year = 2018
                        },
                        new
                        {
                            Id = 10,
                            AccModelId = 9,
                            AccModelName = "porsche_991ii_gt3_cup",
                            Class = "GTC",
                            MaxFuel = 100,
                            MaxRpm = 8500,
                            Name = "Porsche 991 II GT3 Cup",
                            Year = 2017
                        },
                        new
                        {
                            Id = 11,
                            AccModelId = 10,
                            AccModelName = "nissan_gt_r_nismo_gt3",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 7500,
                            Name = "Nissan GT-R Nismo GT3",
                            Year = 2015
                        },
                        new
                        {
                            Id = 12,
                            AccModelId = 11,
                            AccModelName = "bentley_continental_gt3_2016",
                            Class = "GT3",
                            MaxFuel = 132,
                            MaxRpm = 7500,
                            Name = "Bentley Continental GT3",
                            Year = 2016
                        },
                        new
                        {
                            Id = 13,
                            AccModelId = 12,
                            AccModelName = "amr_v12_vantage_gt3",
                            Class = "GT3",
                            MaxFuel = 132,
                            MaxRpm = 7750,
                            Name = "AMR V12 Vantage GT3",
                            Year = 2013
                        },
                        new
                        {
                            Id = 14,
                            AccModelId = 13,
                            AccModelName = "lamborghini_gallardo_rex",
                            Class = "GT3",
                            MaxFuel = 130,
                            MaxRpm = 8650,
                            Name = "Reiter Engineering R-EX GT3",
                            Year = 2017
                        },
                        new
                        {
                            Id = 15,
                            AccModelId = 14,
                            AccModelName = "jaguar_g3",
                            Class = "GT3",
                            MaxFuel = 119,
                            MaxRpm = 8750,
                            Name = "Emil Frey Jaguar G3",
                            Year = 2012
                        },
                        new
                        {
                            Id = 16,
                            AccModelId = 15,
                            AccModelName = "lexus_rc_f_gt3",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 7750,
                            Name = "Lexus RC F GT3",
                            Year = 2016
                        },
                        new
                        {
                            Id = 17,
                            AccModelId = 16,
                            AccModelName = "lamborghini_huracan_gt3_evo",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 8650,
                            Name = "Lamborghini Huracan GT3 Evo",
                            Year = 2019
                        },
                        new
                        {
                            Id = 18,
                            AccModelId = 17,
                            AccModelName = "honda_nsx_gt3",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 7500,
                            Name = "Honda NSX GT3",
                            Year = 2017
                        },
                        new
                        {
                            Id = 19,
                            AccModelId = 18,
                            AccModelName = "lamborghini_huracan_st",
                            Class = "GTC",
                            MaxFuel = 120,
                            MaxRpm = 8650,
                            Name = "Lamborghini Huracan SuperTrofeo",
                            Year = 2015
                        },
                        new
                        {
                            Id = 20,
                            AccModelId = 19,
                            AccModelName = "audi_r8_lms_evo",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 8650,
                            Name = "Audi R8 LMS Evo",
                            Year = 2019
                        },
                        new
                        {
                            Id = 21,
                            AccModelId = 20,
                            AccModelName = "amr_v8_vantage_gt3",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 7250,
                            Name = "AMR V8 Vantage",
                            Year = 2019
                        },
                        new
                        {
                            Id = 22,
                            AccModelId = 21,
                            AccModelName = "honda_nsx_gt3_evo",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 7650,
                            Name = "Honda NSX GT3 Evo",
                            Year = 2019
                        },
                        new
                        {
                            Id = 23,
                            AccModelId = 22,
                            AccModelName = "mclaren_720s_gt3",
                            Class = "GT3",
                            MaxFuel = 125,
                            MaxRpm = 7700,
                            Name = "McLaren 720S GT3",
                            Year = 2019
                        },
                        new
                        {
                            Id = 24,
                            AccModelId = 23,
                            AccModelName = "porsche_991ii_gt3_r",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 9250,
                            Name = "Porsche 991 II GT3 R",
                            Year = 2019
                        },
                        new
                        {
                            Id = 25,
                            AccModelId = 24,
                            AccModelName = "ferrari_488_gt3_evo",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 7600,
                            Name = "Ferrari 488 GT3 Evo",
                            Year = 2020
                        },
                        new
                        {
                            Id = 26,
                            AccModelId = 25,
                            AccModelName = "mercedes_amg_gt3_evo",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 7600,
                            Name = "Mercedes-AMG GT3",
                            Year = 2020
                        },
                        new
                        {
                            Id = 27,
                            AccModelId = 26,
                            AccModelName = "ferrari_488_challenge_evo",
                            Class = "GTC",
                            MaxFuel = 120,
                            MaxRpm = 8000,
                            Name = "Ferrari 488 Challenge Evo",
                            Year = 2020
                        },
                        new
                        {
                            Id = 28,
                            AccModelId = 27,
                            AccModelName = "bmw_m2_cs_racing",
                            Class = "TCX",
                            MaxFuel = 120,
                            MaxRpm = 7520,
                            Name = "BMW M2 Club Sport Racing",
                            Year = 2020
                        },
                        new
                        {
                            Id = 29,
                            AccModelId = 28,
                            AccModelName = "porsche_992_gt3_cup",
                            Class = "GTC",
                            MaxFuel = 120,
                            MaxRpm = 8750,
                            Name = "Porsche 992 GT3 Cup",
                            Year = 2021
                        },
                        new
                        {
                            Id = 30,
                            AccModelId = 29,
                            AccModelName = "lamborghini_huracan_st_evo2",
                            Class = "GTC",
                            MaxFuel = 120,
                            MaxRpm = 8650,
                            Name = "Lamborghini Huracan SuperTrofeo EVO2",
                            Year = 2021
                        },
                        new
                        {
                            Id = 31,
                            AccModelId = 30,
                            AccModelName = "bmw_m4_gt3",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 7000,
                            Name = "BMW M4 GT3",
                            Year = 2022
                        },
                        new
                        {
                            Id = 32,
                            AccModelId = 31,
                            AccModelName = "audi_r8_lms_evo_ii",
                            Class = "GT3",
                            MaxFuel = 120,
                            MaxRpm = 8650,
                            Name = "Audi R8 LMS GT3 Evo 2",
                            Year = 2022
                        },
                        new
                        {
                            Id = 33,
                            AccModelId = 50,
                            AccModelName = "alpine_a110_gt4",
                            Class = "GT4",
                            MaxFuel = 120,
                            MaxRpm = 6450,
                            Name = "Alpine A110 GT4",
                            Year = 2018
                        },
                        new
                        {
                            Id = 34,
                            AccModelId = 51,
                            AccModelName = "amr_v8_vantage_gt4",
                            Class = "GT4",
                            MaxFuel = 120,
                            MaxRpm = 7000,
                            Name = "Aston Martin Vantage GT4",
                            Year = 2018
                        },
                        new
                        {
                            Id = 35,
                            AccModelId = 52,
                            AccModelName = "audi_r8_gt4",
                            Class = "GT4",
                            MaxFuel = 120,
                            MaxRpm = 8650,
                            Name = "Audi R8 GT4",
                            Year = 2018
                        },
                        new
                        {
                            Id = 36,
                            AccModelId = 53,
                            AccModelName = "bmw_m4_gt4",
                            Class = "GT4",
                            MaxFuel = 120,
                            MaxRpm = 7600,
                            Name = "BMW M4 GT4",
                            Year = 2018
                        },
                        new
                        {
                            Id = 37,
                            AccModelId = 55,
                            AccModelName = "chevrolet_camaro_gt4",
                            Class = "GT4",
                            MaxFuel = 120,
                            MaxRpm = 7500,
                            Name = "Chevrolet Camaro GT4",
                            Year = 2017
                        },
                        new
                        {
                            Id = 38,
                            AccModelId = 56,
                            AccModelName = "ginetta_g55_gt4",
                            Class = "GT4",
                            MaxFuel = 120,
                            MaxRpm = 7200,
                            Name = "Ginetta G55 GT4",
                            Year = 2012
                        },
                        new
                        {
                            Id = 39,
                            AccModelId = 57,
                            AccModelName = "ktm_xbow_gt4",
                            Class = "GT4",
                            MaxFuel = 120,
                            MaxRpm = 6500,
                            Name = "KTM X-Bow GT4",
                            Year = 2016
                        },
                        new
                        {
                            Id = 40,
                            AccModelId = 58,
                            AccModelName = "maserati_mc_gt4",
                            Class = "GT4",
                            MaxFuel = 120,
                            MaxRpm = 7000,
                            Name = "Maserati MC GT4",
                            Year = 2016
                        },
                        new
                        {
                            Id = 41,
                            AccModelId = 59,
                            AccModelName = "mclaren_570s_gt4",
                            Class = "GT4",
                            MaxFuel = 120,
                            MaxRpm = 7600,
                            Name = "McLaren 570S GT4",
                            Year = 2016
                        },
                        new
                        {
                            Id = 42,
                            AccModelId = 60,
                            AccModelName = "mercedes_amg_gt4",
                            Class = "GT4",
                            MaxFuel = 120,
                            MaxRpm = 7000,
                            Name = "Mercedes AMG GT4",
                            Year = 2016
                        },
                        new
                        {
                            Id = 43,
                            AccModelId = 61,
                            AccModelName = "porsche_718_cayman_gt4_mr",
                            Class = "GT4",
                            MaxFuel = 120,
                            MaxRpm = 7800,
                            Name = "Porsche 718 Cayman GT4 Clubsport",
                            Year = 2019
                        });
                });

            modelBuilder.Entity("Acc.Server.Results.Companion.Database.Entities.Lap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Car")
                        .HasColumnType("TEXT");

                    b.Property<string>("Driver")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsValid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LapTime")
                        .HasColumnType("TEXT");

                    b.Property<long>("LapTimeMs")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Sector1Time")
                        .HasColumnType("TEXT");

                    b.Property<long>("Sector1TimeMs")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Sector2Time")
                        .HasColumnType("TEXT");

                    b.Property<long>("Sector2TimeMs")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Sector3Time")
                        .HasColumnType("TEXT");

                    b.Property<long>("Sector3TimeMs")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SessionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("Laps");
                });

            modelBuilder.Entity("Acc.Server.Results.Companion.Database.Entities.LeaderBoardLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AverageLapTime")
                        .HasColumnType("TEXT");

                    b.Property<double>("AverageLapTimeMs")
                        .HasColumnType("REAL");

                    b.Property<string>("BestLapTime")
                        .HasColumnType("TEXT");

                    b.Property<long>("BestLapTimeMs")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BestSector1Time")
                        .HasColumnType("TEXT");

                    b.Property<long>("BestSector1TimeMs")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BestSector2Time")
                        .HasColumnType("TEXT");

                    b.Property<long>("BestSector2TimeMs")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BestSector3Time")
                        .HasColumnType("TEXT");

                    b.Property<long>("BestSector3TimeMs")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CarClass")
                        .HasColumnType("TEXT");

                    b.Property<string>("CarName")
                        .HasColumnType("TEXT");

                    b.Property<string>("DriverName")
                        .HasColumnType("TEXT");

                    b.Property<string>("DriverShortName")
                        .HasColumnType("TEXT");

                    b.Property<int>("MissingMandatoryPitStop")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Position")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SessionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TeamName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("LeaderBoardLines");
                });

            modelBuilder.Entity("Acc.Server.Results.Companion.Database.Entities.Penalty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Car")
                        .HasColumnType("TEXT");

                    b.Property<int>("ClearedOnLap")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Driver")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPostRacePenalty")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PenaltyCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("PenaltyValue")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Reason")
                        .HasColumnType("TEXT");

                    b.Property<int>("SessionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ViolationOnLap")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("Penalties");
                });

            modelBuilder.Entity("Acc.Server.Results.Companion.Database.Entities.ServerDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("FtpFolderPath")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsLocalFolder")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("Acc.Server.Results.Companion.Database.Entities.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("BestLapMs")
                        .HasColumnType("INTEGER");

                    b.Property<long>("BestSector1Ms")
                        .HasColumnType("INTEGER");

                    b.Property<long>("BestSector2Ms")
                        .HasColumnType("INTEGER");

                    b.Property<long>("BestSector3Ms")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FilePath")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsWetSession")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MetaData")
                        .HasColumnType("TEXT");

                    b.Property<int>("RaceWeekendIndex")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ServerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ServerName")
                        .HasColumnType("TEXT");

                    b.Property<int>("SessionIndex")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SessionType")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("TrackName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Acc.Server.Results.Companion.Database.Entities.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccTrackId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Corners")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("TrackLength")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Tracks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccTrackId = "barcelona",
                            Corners = 16,
                            Name = "Barcelona",
                            TrackLength = 4655.0
                        },
                        new
                        {
                            Id = 2,
                            AccTrackId = "brands_hatch",
                            Corners = 9,
                            Name = "Brands Hatch",
                            TrackLength = 3908.0
                        },
                        new
                        {
                            Id = 3,
                            AccTrackId = "cota",
                            Corners = 20,
                            Name = "Circuit of the Americas",
                            TrackLength = 5513.0
                        },
                        new
                        {
                            Id = 4,
                            AccTrackId = "donington",
                            Corners = 12,
                            Name = "Donington Park",
                            TrackLength = 4020.0
                        },
                        new
                        {
                            Id = 5,
                            AccTrackId = "hungaroring",
                            Corners = 14,
                            Name = "Hungaroring",
                            TrackLength = 4381.0
                        },
                        new
                        {
                            Id = 6,
                            AccTrackId = "imola",
                            Corners = 22,
                            Name = "Imola",
                            TrackLength = 4959.0
                        },
                        new
                        {
                            Id = 7,
                            AccTrackId = "indianapolis",
                            Corners = 14,
                            Name = "Indianapolis",
                            TrackLength = 4167.0
                        },
                        new
                        {
                            Id = 8,
                            AccTrackId = "kyalami",
                            Corners = 16,
                            Name = "Kyalami",
                            TrackLength = 4522.0
                        },
                        new
                        {
                            Id = 9,
                            AccTrackId = "laguna_seca",
                            Corners = 11,
                            Name = "Laguna Seca",
                            TrackLength = 33602.0
                        },
                        new
                        {
                            Id = 10,
                            AccTrackId = "misano",
                            Corners = 16,
                            Name = "Misano",
                            TrackLength = 4226.0
                        },
                        new
                        {
                            Id = 11,
                            AccTrackId = "monza",
                            Corners = 11,
                            Name = "Monza",
                            TrackLength = 5793.0
                        },
                        new
                        {
                            Id = 12,
                            AccTrackId = "mount_panorama",
                            Corners = 23,
                            Name = "Mount Panorama",
                            TrackLength = 6213.0
                        },
                        new
                        {
                            Id = 13,
                            AccTrackId = "nurburgring",
                            Corners = 17,
                            Name = "Nurburgring",
                            TrackLength = 5173.0
                        },
                        new
                        {
                            Id = 14,
                            AccTrackId = "oulton_park",
                            Corners = 17,
                            Name = "Oulton Park",
                            TrackLength = 4307.0
                        },
                        new
                        {
                            Id = 15,
                            AccTrackId = "paul_ricard",
                            Corners = 13,
                            Name = "Paul Ricard",
                            TrackLength = 5770.0
                        },
                        new
                        {
                            Id = 16,
                            AccTrackId = "silverstone",
                            Corners = 18,
                            Name = "Silverstone",
                            TrackLength = 5891.0
                        },
                        new
                        {
                            Id = 17,
                            AccTrackId = "snetterton",
                            Corners = 12,
                            Name = "Snetterton",
                            TrackLength = 4779.0
                        },
                        new
                        {
                            Id = 18,
                            AccTrackId = "spa",
                            Corners = 19,
                            Name = "Spa Francorchamps",
                            TrackLength = 7004.0
                        },
                        new
                        {
                            Id = 19,
                            AccTrackId = "suzuka",
                            Corners = 18,
                            Name = "Suzuka",
                            TrackLength = 5807.0
                        },
                        new
                        {
                            Id = 20,
                            AccTrackId = "watkins_glen",
                            Corners = 11,
                            Name = "Watkins Glen",
                            TrackLength = 5552.0
                        },
                        new
                        {
                            Id = 21,
                            AccTrackId = "zandvoort",
                            Corners = 14,
                            Name = "Zandvoort",
                            TrackLength = 4252.0
                        },
                        new
                        {
                            Id = 22,
                            AccTrackId = "zolder",
                            Corners = 10,
                            Name = "Zolder",
                            TrackLength = 4011.0
                        });
                });

            modelBuilder.Entity("Acc.Server.Results.Companion.Database.Entities.Lap", b =>
                {
                    b.HasOne("Acc.Server.Results.Companion.Database.Entities.Session", null)
                        .WithMany("Laps")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Acc.Server.Results.Companion.Database.Entities.LeaderBoardLine", b =>
                {
                    b.HasOne("Acc.Server.Results.Companion.Database.Entities.Session", null)
                        .WithMany("LeaderBoardLines")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Acc.Server.Results.Companion.Database.Entities.Penalty", b =>
                {
                    b.HasOne("Acc.Server.Results.Companion.Database.Entities.Session", null)
                        .WithMany("Penalties")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Acc.Server.Results.Companion.Database.Entities.Session", b =>
                {
                    b.HasOne("Acc.Server.Results.Companion.Database.Entities.ServerDetails", null)
                        .WithMany("Sessions")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Acc.Server.Results.Companion.Database.Entities.ServerDetails", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("Acc.Server.Results.Companion.Database.Entities.Session", b =>
                {
                    b.Navigation("Laps");

                    b.Navigation("LeaderBoardLines");

                    b.Navigation("Penalties");
                });
#pragma warning restore 612, 618
        }
    }
}
