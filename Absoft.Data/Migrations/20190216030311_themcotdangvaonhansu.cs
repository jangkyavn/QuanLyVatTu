using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class themcotdangvaonhansu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dang",
                table: "NhanSu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dang",
                table: "NhanSu");
        }
    }
}
