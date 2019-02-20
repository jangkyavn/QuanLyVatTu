using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class AddSomeColumnForImportDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BietDuoc",
                table: "NhapChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HanDung",
                table: "NhapChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoLo",
                table: "NhapChiTiet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BietDuoc",
                table: "NhapChiTiet");

            migrationBuilder.DropColumn(
                name: "HanDung",
                table: "NhapChiTiet");

            migrationBuilder.DropColumn(
                name: "SoLo",
                table: "NhapChiTiet");
        }
    }
}
