using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class themchietkhauxuatvattu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NguonGoc",
                table: "NhapChiTiet");

            migrationBuilder.AddColumn<int>(
                name: "ChietKhau",
                table: "XuatVatTu",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaNguon",
                table: "NhapChiTiet",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChietKhau",
                table: "XuatVatTu");

            migrationBuilder.DropColumn(
                name: "MaNguon",
                table: "NhapChiTiet");

            migrationBuilder.AddColumn<string>(
                name: "NguonGoc",
                table: "NhapChiTiet",
                nullable: true);
        }
    }
}
