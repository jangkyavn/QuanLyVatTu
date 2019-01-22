using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhoHangs",
                columns: table => new
                {
                    MaKho = table.Column<int>(nullable: false),
                    MaPhieuNhap = table.Column<int>(nullable: false),
                    MaVatTu = table.Column<int>(nullable: false),
                    SoLuongTon = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoHangs", x => new { x.MaKho, x.MaPhieuNhap, x.MaVatTu });
                });

            migrationBuilder.CreateTable(
                name: "NhapChiTiets",
                columns: table => new
                {
                    MaPhieuNhap = table.Column<int>(nullable: false),
                    MaVatTu = table.Column<int>(nullable: false),
                    MaNuoc = table.Column<int>(nullable: false),
                    MaHang = table.Column<int>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    Seri = table.Column<string>(nullable: true),
                    SoKhung = table.Column<string>(nullable: true),
                    SoMay = table.Column<string>(nullable: true),
                    SoDangKy = table.Column<string>(nullable: true),
                    DotMua = table.Column<string>(nullable: true),
                    NamSX = table.Column<string>(nullable: true),
                    PhanCap = table.Column<string>(nullable: true),
                    NguonGoc = table.Column<string>(nullable: true),
                    SoLuong = table.Column<int>(nullable: false),
                    DonGia = table.Column<decimal>(nullable: false),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhapChiTiets", x => new { x.MaPhieuNhap, x.MaVatTu });
                });

            migrationBuilder.CreateTable(
                name: "NhapVatTus",
                columns: table => new
                {
                    MaPhieuNhap = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaHM = table.Column<int>(nullable: false),
                    MaKho = table.Column<int>(nullable: false),
                    NgayNhap = table.Column<string>(nullable: true),
                    NguoiNhap = table.Column<string>(nullable: true),
                    TongSoTien = table.Column<decimal>(nullable: false),
                    TongSoLuong = table.Column<int>(nullable: false),
                    ChietKhau = table.Column<int>(nullable: false),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhapVatTus", x => x.MaPhieuNhap);
                });

            migrationBuilder.CreateTable(
                name: "XuatChiTiets",
                columns: table => new
                {
                    MaPhieuXuat = table.Column<int>(nullable: false),
                    MaPhieuNhap = table.Column<int>(nullable: false),
                    MaVatTu = table.Column<int>(nullable: false),
                    SoLuongXuat = table.Column<int>(nullable: false),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XuatChiTiets", x => new { x.MaPhieuXuat, x.MaPhieuNhap, x.MaVatTu });
                });

            migrationBuilder.CreateTable(
                name: "XuatVatTus",
                columns: table => new
                {
                    MaPhieuXuat = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaKho = table.Column<int>(nullable: false),
                    MaNS = table.Column<int>(nullable: false),
                    NgayNhap = table.Column<string>(nullable: true),
                    TongSoTien = table.Column<decimal>(nullable: false),
                    TongSoLuong = table.Column<int>(nullable: false),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XuatVatTus", x => x.MaPhieuXuat);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KhoHangs");

            migrationBuilder.DropTable(
                name: "NhapChiTiets");

            migrationBuilder.DropTable(
                name: "NhapVatTus");

            migrationBuilder.DropTable(
                name: "XuatChiTiets");

            migrationBuilder.DropTable(
                name: "XuatVatTus");
        }
    }
}
