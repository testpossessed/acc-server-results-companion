using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acc.Server.Results.Companion.Migrations
{
    /// <inheritdoc />
    public partial class AddLapsAndPenalties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Laps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Car = table.Column<string>(type: "TEXT", nullable: true),
                    Driver = table.Column<string>(type: "TEXT", nullable: true),
                    LapTime = table.Column<string>(type: "TEXT", nullable: true),
                    LapTimeMs = table.Column<long>(type: "INTEGER", nullable: false),
                    Sector1Time = table.Column<string>(type: "TEXT", nullable: true),
                    Sector1TimeMs = table.Column<long>(type: "INTEGER", nullable: false),
                    Sector2Time = table.Column<string>(type: "TEXT", nullable: true),
                    Sector2TimeMs = table.Column<long>(type: "INTEGER", nullable: false),
                    Sector3Time = table.Column<string>(type: "TEXT", nullable: true),
                    Sector3TimeMs = table.Column<long>(type: "INTEGER", nullable: false),
                    SessionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Laps_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Penalties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Car = table.Column<string>(type: "TEXT", nullable: true),
                    ClearedOnLap = table.Column<int>(type: "INTEGER", nullable: false),
                    Driver = table.Column<string>(type: "TEXT", nullable: true),
                    IsPostRacePenalty = table.Column<bool>(type: "INTEGER", nullable: false),
                    PenaltyCode = table.Column<string>(type: "TEXT", nullable: true),
                    PenaltyValue = table.Column<int>(type: "INTEGER", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", nullable: true),
                    SessionId = table.Column<int>(type: "INTEGER", nullable: false),
                    ViolationOnLap = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Penalties_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Laps_SessionId",
                table: "Laps",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_SessionId",
                table: "Penalties",
                column: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Laps");

            migrationBuilder.DropTable(
                name: "Penalties");
        }
    }
}
