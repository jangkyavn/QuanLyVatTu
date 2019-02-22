using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class AddForeignKeyForNhapChiTiet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NhapChiTiet_MaHang",
                table: "NhapChiTiet",
                column: "MaHang");

            migrationBuilder.CreateIndex(
                name: "IX_NhapChiTiet_MaNguon",
                table: "NhapChiTiet",
                column: "MaNguon");

            migrationBuilder.CreateIndex(
                name: "IX_NhapChiTiet_MaNuoc",
                table: "NhapChiTiet",
                column: "MaNuoc");

            migrationBuilder.AddForeignKey(
                name: "FK_NhapChiTiet_HangSanXuat_MaHang",
                table: "NhapChiTiet",
                column: "MaHang",
                principalTable: "HangSanXuat",
                principalColumn: "MaHang",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NhapChiTiet_NguonCungCap_MaNguon",
                table: "NhapChiTiet",
                column: "MaNguon",
                principalTable: "NguonCungCap",
                principalColumn: "MaNguon",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NhapChiTiet_NuocSanXuat_MaNuoc",
                table: "NhapChiTiet",
                column: "MaNuoc",
                principalTable: "NuocSanXuat",
                principalColumn: "MaNuoc",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhapChiTiet_HangSanXuat_MaHang",
                table: "NhapChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_NhapChiTiet_NguonCungCap_MaNguon",
                table: "NhapChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_NhapChiTiet_NuocSanXuat_MaNuoc",
                table: "NhapChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_NhapChiTiet_MaHang",
                table: "NhapChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_NhapChiTiet_MaNguon",
                table: "NhapChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_NhapChiTiet_MaNuoc",
                table: "NhapChiTiet");
        }
    }
}
