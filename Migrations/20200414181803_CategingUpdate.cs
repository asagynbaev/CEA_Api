using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class CategingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Organization",
                table: "Catering");

            migrationBuilder.AddColumn<int>(
                name: "AmountOfPeople",
                table: "Catering",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CatDate",
                table: "Catering",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OrganizationName",
                table: "Catering",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfPeople",
                table: "Catering");

            migrationBuilder.DropColumn(
                name: "CatDate",
                table: "Catering");

            migrationBuilder.DropColumn(
                name: "OrganizationName",
                table: "Catering");

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "Catering",
                type: "text",
                nullable: true);
        }
    }
}
