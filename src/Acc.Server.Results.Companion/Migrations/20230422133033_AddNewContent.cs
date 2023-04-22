using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Acc.Server.Results.Companion.Migrations
{
    /// <inheritdoc />
    public partial class AddNewContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AccModelId", "AccModelName", "Class", "MaxFuel", "MaxRpm", "Name", "Year" },
                values: new object[,]
                {
                    { 44, 32, "ferrari_296_gt3", "GT3", 120, 7300, "Ferrari 296 GT3", 2023 },
                    { 45, 33, "lamborghini_huracan_gt3_evo2", "GT3", 120, 8650, "Lamborghini Huracan GT3 Evo 2", 2023 },
                    { 46, 34, "porsche_992_gt3_r", "GT3", 120, 9250, "Porsche 992 GT3 R", 2023 }
                });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "AccTrackId", "Corners", "Name", "TrackLength" },
                values: new object[] { 23, "valencia", 14, "Valencia", 4005.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: 23);
        }
    }
}
