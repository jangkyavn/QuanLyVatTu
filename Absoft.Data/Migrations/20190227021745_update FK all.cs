using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class updateFKall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThanhLyChiTiet_NhapVatTu_MaPhieuNhap",
                table: "ThanhLyChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_ThanhLyChiTiet_ThanhLyVatTu_MaPhieuThanhLy",
                table: "ThanhLyChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_ThanhLyChiTiet_VatTu_MaVatTu",
                table: "ThanhLyChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_XuatChiTiet_MaPhieuNhap",
                table: "XuatChiTiet",
                column: "MaPhieuNhap");

            migrationBuilder.CreateIndex(
                name: "IX_XuatChiTiet_MaVatTu",
                table: "XuatChiTiet",
                column: "MaVatTu");

            migrationBuilder.CreateIndex(
                name: "IX_NhapChiTiet_MaVatTu",
                table: "NhapChiTiet",
                column: "MaVatTu");

            migrationBuilder.CreateIndex(
                name: "IX_KhoHang_MaPhieuNhap",
                table: "KhoHang",
                column: "MaPhieuNhap");

            migrationBuilder.CreateIndex(
                name: "IX_KhoHang_MaVatTu",
                table: "KhoHang",
                column: "MaVatTu");

            migrationBuilder.AddForeignKey(
                name: "FK_KhoHang_KhoVatTu_MaKho",
                table: "KhoHang",
                column: "MaKho",
                principalTable: "KhoVatTu",
                principalColumn: "MaKho",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KhoHang_NhapVatTu_MaPhieuNhap",
                table: "KhoHang",
                column: "MaPhieuNhap",
                principalTable: "NhapVatTu",
                principalColumn: "MaPhieuNhap",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KhoHang_VatTu_MaVatTu",
                table: "KhoHang",
                column: "MaVatTu",
                principalTable: "VatTu",
                principalColumn: "MaVatTu",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NhapChiTiet_NhapVatTu_MaPhieuNhap",
                table: "NhapChiTiet",
                column: "MaPhieuNhap",
                principalTable: "NhapVatTu",
                principalColumn: "MaPhieuNhap",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NhapChiTiet_VatTu_MaVatTu",
                table: "NhapChiTiet",
                column: "MaVatTu",
                principalTable: "VatTu",
                principalColumn: "MaVatTu",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhLyChiTiet_NhapVatTu_MaPhieuNhap",
                table: "ThanhLyChiTiet",
                column: "MaPhieuNhap",
                principalTable: "NhapVatTu",
                principalColumn: "MaPhieuNhap",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhLyChiTiet_ThanhLyVatTu_MaPhieuThanhLy",
                table: "ThanhLyChiTiet",
                column: "MaPhieuThanhLy",
                principalTable: "ThanhLyVatTu",
                principalColumn: "MaPhieuThanhLy",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhLyChiTiet_VatTu_MaVatTu",
                table: "ThanhLyChiTiet",
                column: "MaVatTu",
                principalTable: "VatTu",
                principalColumn: "MaVatTu",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_XuatChiTiet_NhapVatTu_MaPhieuNhap",
                table: "XuatChiTiet",
                column: "MaPhieuNhap",
                principalTable: "NhapVatTu",
                principalColumn: "MaPhieuNhap",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_XuatChiTiet_XuatVatTu_MaPhieuXuat",
                table: "XuatChiTiet",
                column: "MaPhieuXuat",
                principalTable: "XuatVatTu",
                principalColumn: "MaPhieuXuat",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_XuatChiTiet_VatTu_MaVatTu",
                table: "XuatChiTiet",
                column: "MaVatTu",
                principalTable: "VatTu",
                principalColumn: "MaVatTu",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KhoHang_KhoVatTu_MaKho",
                table: "KhoHang");

            migrationBuilder.DropForeignKey(
                name: "FK_KhoHang_NhapVatTu_MaPhieuNhap",
                table: "KhoHang");

            migrationBuilder.DropForeignKey(
                name: "FK_KhoHang_VatTu_MaVatTu",
                table: "KhoHang");

            migrationBuilder.DropForeignKey(
                name: "FK_NhapChiTiet_NhapVatTu_MaPhieuNhap",
                table: "NhapChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_NhapChiTiet_VatTu_MaVatTu",
                table: "NhapChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_ThanhLyChiTiet_NhapVatTu_MaPhieuNhap",
                table: "ThanhLyChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_ThanhLyChiTiet_ThanhLyVatTu_MaPhieuThanhLy",
                table: "ThanhLyChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_ThanhLyChiTiet_VatTu_MaVatTu",
                table: "ThanhLyChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_XuatChiTiet_NhapVatTu_MaPhieuNhap",
                table: "XuatChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_XuatChiTiet_XuatVatTu_MaPhieuXuat",
                table: "XuatChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_XuatChiTiet_VatTu_MaVatTu",
                table: "XuatChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_XuatChiTiet_MaPhieuNhap",
                table: "XuatChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_XuatChiTiet_MaVatTu",
                table: "XuatChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_NhapChiTiet_MaVatTu",
                table: "NhapChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_KhoHang_MaPhieuNhap",
                table: "KhoHang");

            migrationBuilder.DropIndex(
                name: "IX_KhoHang_MaVatTu",
                table: "KhoHang");

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhLyChiTiet_NhapVatTu_MaPhieuNhap",
                table: "ThanhLyChiTiet",
                column: "MaPhieuNhap",
                principalTable: "NhapVatTu",
                principalColumn: "MaPhieuNhap",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhLyChiTiet_ThanhLyVatTu_MaPhieuThanhLy",
                table: "ThanhLyChiTiet",
                column: "MaPhieuThanhLy",
                principalTable: "ThanhLyVatTu",
                principalColumn: "MaPhieuThanhLy",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhLyChiTiet_VatTu_MaVatTu",
                table: "ThanhLyChiTiet",
                column: "MaVatTu",
                principalTable: "VatTu",
                principalColumn: "MaVatTu",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
