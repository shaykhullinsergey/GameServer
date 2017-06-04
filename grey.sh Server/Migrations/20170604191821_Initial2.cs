using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace grey.shServer.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BattleToken",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "SearchingForBattle",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Players");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BattleToken",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SearchingForBattle",
                table: "Players",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Players",
                nullable: true);
        }
    }
}
