using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class asdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TFrom",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "TTo",
                table: "Positions");

            migrationBuilder.AddColumn<DateTime>(
                name: "DefaultTime",
                table: "Positions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultTime",
                table: "Positions");

            migrationBuilder.AddColumn<DateTime>(
                name: "TFrom",
                table: "Positions",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TTo",
                table: "Positions",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
