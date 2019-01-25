using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class addstatuscacbang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "XuatVatTu",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "XuatChiTiet",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "VatTu",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "NuocSanXuat",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "NhapChiTiet",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "NhanSu",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "NguonCungCap",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "LoaiVatTu",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "KhoVatTu",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "KhoHang",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "HangSanXuat",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "HangMucVatTu",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "DonViTinh",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "XuatVatTu");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "XuatChiTiet");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VatTu");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NuocSanXuat");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NhapChiTiet");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NhanSu");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NguonCungCap");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LoaiVatTu");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "KhoVatTu");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "KhoHang");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "HangSanXuat");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "HangMucVatTu");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DonViTinh");
        }
    }
}
