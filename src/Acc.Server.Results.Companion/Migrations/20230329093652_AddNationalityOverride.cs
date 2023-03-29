using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acc.Server.Results.Companion.Migrations
{
    /// <inheritdoc />
    public partial class AddNationalityOverride : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DriverNationalityCodeOverride",
                table: "Drivers",
                newName: "NationalityCodeOverride");

            migrationBuilder.AddColumn<string>(
                name: "NationalityOverride",
                table: "Drivers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalityOverride",
                table: "Drivers");

            migrationBuilder.RenameColumn(
                name: "NationalityCodeOverride",
                table: "Drivers",
                newName: "DriverNationalityCodeOverride");
        }
    }
}
