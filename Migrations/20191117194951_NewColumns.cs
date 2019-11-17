using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class NewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "Employees",
                newName: "FirstName");

            migrationBuilder.AddColumn<bool>(
                name: "Buses",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Hotels",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Shops",
                table: "Employees",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Buses",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Hotels",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Shops",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Employees",
                newName: "Firstname");
        }
    }
}
