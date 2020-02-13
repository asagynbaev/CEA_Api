using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class TTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeFrom",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "TimeTo",
                table: "Positions");

            migrationBuilder.AddColumn<DateTime>(
                name: "TFrom",
                table: "Positions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TTo",
                table: "Positions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TFrom",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "TTo",
                table: "Positions");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeFrom",
                table: "Positions",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeTo",
                table: "Positions",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
