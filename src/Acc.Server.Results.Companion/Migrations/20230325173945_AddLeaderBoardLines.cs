using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Acc.Server.Results.Companion.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaderBoardLines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BestLapMs",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BestSector1Ms",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BestSector2Ms",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BestSector3Ms",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsWetSession",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Class = table.Column<string>(type: "TEXT", nullable: true),
                    AccModelName = table.Column<string>(type: "TEXT", nullable: true),
                    MaxFuel = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxRpm = table.Column<int>(type: "INTEGER", nullable: false),
                    AccModelId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaderBoardLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AverageLapTime = table.Column<string>(type: "TEXT", nullable: true),
                    AverageLapTimeMs = table.Column<double>(type: "REAL", nullable: false),
                    BestLapTime = table.Column<string>(type: "TEXT", nullable: true),
                    BestLapTimeMs = table.Column<long>(type: "INTEGER", nullable: false),
                    BestSector1Time = table.Column<string>(type: "TEXT", nullable: true),
                    BestSector1TimeMs = table.Column<long>(type: "INTEGER", nullable: false),
                    BestSector2Time = table.Column<string>(type: "TEXT", nullable: true),
                    BestSector2TimeMs = table.Column<long>(type: "INTEGER", nullable: false),
                    BestSector3Time = table.Column<string>(type: "TEXT", nullable: true),
                    BestSector3TimeMs = table.Column<long>(type: "INTEGER", nullable: false),
                    CarClass = table.Column<string>(type: "TEXT", nullable: true),
                    CarName = table.Column<string>(type: "TEXT", nullable: true),
                    DriverName = table.Column<string>(type: "TEXT", nullable: true),
                    DriverShortName = table.Column<string>(type: "TEXT", nullable: true),
                    MissingMandatoryPitStop = table.Column<int>(type: "INTEGER", nullable: false),
                    SessionId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderBoardLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaderBoardLines_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccTrackId = table.Column<string>(type: "TEXT", nullable: true),
                    Corners = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    TrackLength = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AccModelId", "AccModelName", "Class", "MaxFuel", "MaxRpm", "Name", "Year" },
                values: new object[,]
                {
                    { 1, 0, "porsche_991_gt3_r", "GT3", 120, 9250, "Porsche 991 GT3 R", 2018 },
                    { 2, 1, "mercedes_amg_gt3", "GT3", 120, 7900, "Mercedes-AMG GT3", 2015 },
                    { 3, 2, "ferrari_488_gt3", "GT3", 110, 7300, "Ferrari 488 GT3", 2018 },
                    { 4, 3, "audi_r8_lms", "GT3", 120, 8650, "Audi R8 LMS", 2015 },
                    { 5, 4, "lamborghini_huracán_gt3", "GT3", 120, 8650, "Lamborghini Huracán GT3", 2015 },
                    { 6, 5, "mclaren_650s_gt3", "GT3", 125, 7500, "McLaren 650S GT3", 2015 },
                    { 7, 6, "nissan_gt_r_nismo_gt3", "GT3", 132, 7500, "Nissan GT-R Nismo GT3", 2018 },
                    { 8, 7, "bmw_m6_gt3", "GT3", 125, 7100, "BMW M6 GT3", 2017 },
                    { 9, 8, "bentley_continental_gt3_2018", "GT3", 132, 7400, "Bentley Continental GT3", 2018 },
                    { 10, 9, "porsche_991ii_gt3_cup", "GTC", 100, 8500, "Porsche 991 II GT3 Cup", 2017 },
                    { 11, 10, "nissan_gt_r_nismo_gt3", "GT3", 120, 7500, "Nissan GT-R Nismo GT3", 2015 },
                    { 12, 11, "bentley_continental_gt3_2016", "GT3", 132, 7500, "Bentley Continental GT3", 2016 },
                    { 13, 12, "amr_v12_vantage_gt3", "GT3", 132, 7750, "AMR V12 Vantage GT3", 2013 },
                    { 14, 13, "lamborghini_gallardo_rex", "GT3", 130, 8650, "Reiter Engineering R-EX GT3", 2017 },
                    { 15, 14, "jaguar_g3", "GT3", 119, 8750, "Emil Frey Jaguar G3", 2012 },
                    { 16, 15, "lexus_rc_f_gt3", "GT3", 120, 7750, "Lexus RC F GT3", 2016 },
                    { 17, 16, "lamborghini_huracan_gt3_evo", "GT3", 120, 8650, "Lamborghini Huracan GT3 Evo", 2019 },
                    { 18, 17, "honda_nsx_gt3", "GT3", 120, 7500, "Honda NSX GT3", 2017 },
                    { 19, 18, "lamborghini_huracan_st", "GTC", 120, 8650, "Lamborghini Huracan SuperTrofeo", 2015 },
                    { 20, 19, "audi_r8_lms_evo", "GT3", 120, 8650, "Audi R8 LMS Evo", 2019 },
                    { 21, 20, "amr_v8_vantage_gt3", "GT3", 120, 7250, "AMR V8 Vantage", 2019 },
                    { 22, 21, "honda_nsx_gt3_evo", "GT3", 120, 7650, "Honda NSX GT3 Evo", 2019 },
                    { 23, 22, "mclaren_720s_gt3", "GT3", 125, 7700, "McLaren 720S GT3", 2019 },
                    { 24, 23, "porsche_991ii_gt3_r", "GT3", 120, 9250, "Porsche 991 II GT3 R", 2019 },
                    { 25, 24, "ferrari_488_gt3_evo", "GT3", 120, 7600, "Ferrari 488 GT3 Evo", 2020 },
                    { 26, 25, "mercedes_amg_gt3_evo", "GT3", 120, 7600, "Mercedes-AMG GT3", 2020 },
                    { 27, 26, "ferrari_488_challenge_evo", "GTC", 120, 8000, "Ferrari 488 Challenge Evo", 2020 },
                    { 28, 27, "bmw_m2_cs_racing", "TCX", 120, 7520, "BMW M2 Club Sport Racing", 2020 },
                    { 29, 28, "porsche_992_gt3_cup", "GTC", 120, 8750, "Porsche 992 GT3 Cup", 2021 },
                    { 30, 29, "lamborghini_huracan_st_evo2", "GTC", 120, 8650, "Lamborghini Huracan SuperTrofeo EVO2", 2021 },
                    { 31, 30, "bmw_m4_gt3", "GT3", 120, 7000, "BMW M4 GT3", 2022 },
                    { 32, 31, "audi_r8_lms_evo_ii", "GT3", 120, 8650, "Audi R8 LMS GT3 Evo 2", 2022 },
                    { 33, 50, "alpine_a110_gt4", "GT4", 120, 6450, "Alpine A110 GT4", 2018 },
                    { 34, 51, "amr_v8_vantage_gt4", "GT4", 120, 7000, "Aston Martin Vantage GT4", 2018 },
                    { 35, 52, "audi_r8_gt4", "GT4", 120, 8650, "Audi R8 GT4", 2018 },
                    { 36, 53, "bmw_m4_gt4", "GT4", 120, 7600, "BMW M4 GT4", 2018 },
                    { 37, 55, "chevrolet_camaro_gt4", "GT4", 120, 7500, "Chevrolet Camaro GT4", 2017 },
                    { 38, 56, "ginetta_g55_gt4", "GT4", 120, 7200, "Ginetta G55 GT4", 2012 },
                    { 39, 57, "ktm_xbow_gt4", "GT4", 120, 6500, "KTM X-Bow GT4", 2016 },
                    { 40, 58, "maserati_mc_gt4", "GT4", 120, 7000, "Maserati MC GT4", 2016 },
                    { 41, 59, "mclaren_570s_gt4", "GT4", 120, 7600, "McLaren 570S GT4", 2016 },
                    { 42, 60, "mercedes_amg_gt4", "GT4", 120, 7000, "Mercedes AMG GT4", 2016 },
                    { 43, 61, "porsche_718_cayman_gt4_mr", "GT4", 120, 7800, "Porsche 718 Cayman GT4 Clubsport", 2019 }
                });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "AccTrackId", "Corners", "Name", "TrackLength" },
                values: new object[,]
                {
                    { 1, "barcelona", 16, "Barcelona", 4655.0 },
                    { 2, "brands_hatch", 9, "Brands Hatch", 3908.0 },
                    { 3, "cota", 20, "Circuit of the Americas", 5513.0 },
                    { 4, "donington", 12, "Donington Park", 4020.0 },
                    { 5, "hungaroring", 14, "Hungaroring", 4381.0 },
                    { 6, "imola", 22, "Imola", 4959.0 },
                    { 7, "indianapolis", 14, "Indianapolis", 4167.0 },
                    { 8, "kyalami", 16, "Kyalami", 4522.0 },
                    { 9, "laguna_seca", 11, "Laguna Seca", 33602.0 },
                    { 10, "misano", 16, "Misano", 4226.0 },
                    { 11, "monza", 11, "Monza", 5793.0 },
                    { 12, "mount_panorama", 23, "Mount Panorama", 6213.0 },
                    { 13, "nurburgring", 17, "Nurburgring", 5173.0 },
                    { 14, "oulton_park", 17, "Oulton Park", 4307.0 },
                    { 15, "paul_ricard", 13, "Paul Ricard", 5770.0 },
                    { 16, "silverstone", 18, "Silverstone", 5891.0 },
                    { 17, "snetterton", 12, "Snetterton", 4779.0 },
                    { 18, "spa", 19, "Spa Francorchamps", 7004.0 },
                    { 19, "suzuka", 18, "Suzuka", 5807.0 },
                    { 20, "watkins_glen", 11, "Watkins Glen", 5552.0 },
                    { 21, "zandvoort", 14, "Zandvoort", 4252.0 },
                    { 22, "zolder", 10, "Zolder", 4011.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaderBoardLines_SessionId",
                table: "LeaderBoardLines",
                column: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "LeaderBoardLines");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropColumn(
                name: "BestLapMs",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "BestSector1Ms",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "BestSector2Ms",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "BestSector3Ms",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "IsWetSession",
                table: "Sessions");
        }
    }
}
