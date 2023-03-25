﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acc.Server.Results.Companion.Migrations
{
    /// <inheritdoc />
    public partial class ExtendLeaderBoardLines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "LeaderBoardLines",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "LeaderBoardLines");
        }
    }
}
