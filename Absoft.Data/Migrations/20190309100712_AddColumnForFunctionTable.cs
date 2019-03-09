using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class AddColumnForFunctionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasCreate",
                table: "ChucNang",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDelete",
                table: "ChucNang",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasRead",
                table: "ChucNang",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasUpdate",
                table: "ChucNang",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasCreate",
                table: "ChucNang");

            migrationBuilder.DropColumn(
                name: "HasDelete",
                table: "ChucNang");

            migrationBuilder.DropColumn(
                name: "HasRead",
                table: "ChucNang");

            migrationBuilder.DropColumn(
                name: "HasUpdate",
                table: "ChucNang");
        }
    }
}
