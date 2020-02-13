using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class TimeTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultTime",
                table: "Positions");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeFrom",
                table: "Positions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeTo",
                table: "Positions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeFrom",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "TimeTo",
                table: "Positions");

            migrationBuilder.AddColumn<DateTime>(
                name: "DefaultTime",
                table: "Positions",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
