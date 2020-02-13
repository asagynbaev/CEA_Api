using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class CancelShift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CanceledAt",
                table: "Shifts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CanceledBy",
                table: "Shifts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Shifts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanceledAt",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "CanceledBy",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Shifts");
        }
    }
}
