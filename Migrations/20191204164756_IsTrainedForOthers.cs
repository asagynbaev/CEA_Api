using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class IsTrainedForOthers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTrained",
                table: "Employees");

            migrationBuilder.AddColumn<bool>(
                name: "BusesTrain",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HotelsTrain",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShopsTrain",
                table: "Employees",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusesTrain",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "HotelsTrain",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ShopsTrain",
                table: "Employees");

            migrationBuilder.AddColumn<bool>(
                name: "IsTrained",
                table: "Employees",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
