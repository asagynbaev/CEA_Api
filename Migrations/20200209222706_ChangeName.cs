using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class ChangeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionName",
                table: "Positions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Positions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Positions");

            migrationBuilder.AddColumn<string>(
                name: "PositionName",
                table: "Positions",
                type: "text",
                nullable: true);
        }
    }
}
