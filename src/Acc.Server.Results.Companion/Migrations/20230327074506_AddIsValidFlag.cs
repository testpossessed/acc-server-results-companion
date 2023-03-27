using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acc.Server.Results.Companion.Migrations
{
    /// <inheritdoc />
    public partial class AddIsValidFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Laps",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Laps");
        }
    }
}
