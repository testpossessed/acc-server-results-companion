using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acc.Server.Results.Companion.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryClassificationOverridesToServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BronzeClassification",
                table: "Servers",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "AM");

            migrationBuilder.AddColumn<string>(
                name: "GoldClassification",
                table: "Servers",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "PRO");

            migrationBuilder.AddColumn<string>(
                name: "PlatinumClassification",
                table: "Servers",
                type: "TEXT",
                nullable: false, defaultValueSql: "PRO");

            migrationBuilder.AddColumn<string>(
                name: "SilverClassification",
                table: "Servers",
                type: "TEXT",
                nullable: false, defaultValueSql: "PRO-AM");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BronzeClassification",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "GoldClassification",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "PlatinumClassification",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "SilverClassification",
                table: "Servers");
        }
    }
}
