using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acc.Server.Results.Companion.Migrations
{
    /// <inheritdoc />
    public partial class AddDriverOverrides : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverNationalityCodeOverride",
                table: "Drivers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameOverride",
                table: "Drivers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsImported",
                table: "Drivers",
                type: "INTEGER",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNameOverride",
                table: "Drivers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OurCategory",
                table: "Drivers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverNationalityCodeOverride",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "FirstNameOverride",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "IsImported",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "LastNameOverride",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "OurCategory",
                table: "Drivers");
        }
    }
}
