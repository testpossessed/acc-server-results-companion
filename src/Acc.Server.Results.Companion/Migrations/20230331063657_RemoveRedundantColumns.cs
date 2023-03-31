﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acc.Server.Results.Companion.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRedundantColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OurCategory",
                table: "Penalties");

            migrationBuilder.DropColumn(
                name: "OurCategory",
                table: "LeaderBoardLines");

            migrationBuilder.DropColumn(
                name: "OurCategory",
                table: "Laps");

            migrationBuilder.DropColumn(
                name: "DriverCategory",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "OurCategory",
                table: "Drivers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OurCategory",
                table: "Penalties",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OurCategory",
                table: "LeaderBoardLines",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OurCategory",
                table: "Laps",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverCategory",
                table: "Drivers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OurCategory",
                table: "Drivers",
                type: "TEXT",
                nullable: true);
        }
    }
}
