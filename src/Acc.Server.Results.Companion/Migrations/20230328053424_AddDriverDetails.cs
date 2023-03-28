using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acc.Server.Results.Companion.Migrations
{
    /// <inheritdoc />
    public partial class AddDriverDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverCategory",
                table: "Penalties",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Penalties",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NationalityCode",
                table: "Penalties",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DriverCategory",
                table: "LeaderBoardLines",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "LeaderBoardLines",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NationalityCode",
                table: "LeaderBoardLines",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DriverCategory",
                table: "Laps",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Laps",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NationalityCode",
                table: "Laps",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DriverCategory = table.Column<string>(type: "TEXT", nullable: true),
                    DriverCategoryCode = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Nationality = table.Column<string>(type: "TEXT", nullable: true),
                    NationalityCode = table.Column<int>(type: "INTEGER", nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropColumn(
                name: "DriverCategory",
                table: "Penalties");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Penalties");

            migrationBuilder.DropColumn(
                name: "NationalityCode",
                table: "Penalties");

            migrationBuilder.DropColumn(
                name: "DriverCategory",
                table: "LeaderBoardLines");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "LeaderBoardLines");

            migrationBuilder.DropColumn(
                name: "NationalityCode",
                table: "LeaderBoardLines");

            migrationBuilder.DropColumn(
                name: "DriverCategory",
                table: "Laps");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Laps");

            migrationBuilder.DropColumn(
                name: "NationalityCode",
                table: "Laps");
        }
    }
}
