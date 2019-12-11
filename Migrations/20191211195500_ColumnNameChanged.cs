using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class ColumnNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Helpers");

            migrationBuilder.AddColumn<string>(
                name: "HelperName",
                table: "Helpers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HelperName",
                table: "Helpers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Helpers",
                type: "text",
                nullable: true);
        }
    }
}
