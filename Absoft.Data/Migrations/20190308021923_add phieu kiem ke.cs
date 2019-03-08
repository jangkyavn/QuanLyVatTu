using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class addphieukiemke : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KiemKeVatTu",
                columns: table => new
                {
                    MaPhieuKiemKe = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SoPhieuKiemKe = table.Column<string>(nullable: true),
                    MaKho = table.Column<int>(nullable: false),
                    MaNS = table.Column<int>(nullable: false),
                    NgayKiemK = table.Column<string>(nullable: true),
                    TongTheoDoi = table.Column<int>(nullable: true),
                    TongThucTon = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KiemKeVatTu", x => x.MaPhieuKiemKe);
                    table.ForeignKey(
                        name: "FK_KiemKeVatTu_KhoVatTu_MaKho",
                        column: x => x.MaKho,
                        principalTable: "KhoVatTu",
                        principalColumn: "MaKho",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KiemKeVatTu_NhanSu_MaNS",
                        column: x => x.MaNS,
                        principalTable: "NhanSu",
                        principalColumn: "MaNS",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KiemKeChiTiet",
                columns: table => new
                {
                    MaPhieuKiemKe = table.Column<int>(nullable: false),
                    MaPhieuNhap = table.Column<int>(nullable: false),
                    MaVatTu = table.Column<int>(nullable: false),
                    SoLuongTheoDoi = table.Column<int>(nullable: true),
                    SoLuongThucTon = table.Column<int>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KiemKeChiTiet", x => new { x.MaPhieuKiemKe, x.MaPhieuNhap, x.MaVatTu });
                    table.ForeignKey(
                        name: "FK_KiemKeChiTiet_KiemKeVatTu_MaPhieuKiemKe",
                        column: x => x.MaPhieuKiemKe,
                        principalTable: "KiemKeVatTu",
                        principalColumn: "MaPhieuKiemKe",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KiemKeChiTiet_NhapVatTu_MaPhieuNhap",
                        column: x => x.MaPhieuNhap,
                        principalTable: "NhapVatTu",
                        principalColumn: "MaPhieuNhap",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KiemKeChiTiet_VatTu_MaVatTu",
                        column: x => x.MaVatTu,
                        principalTable: "VatTu",
                        principalColumn: "MaVatTu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KiemKeChiTiet_MaPhieuNhap",
                table: "KiemKeChiTiet",
                column: "MaPhieuNhap");

            migrationBuilder.CreateIndex(
                name: "IX_KiemKeChiTiet_MaVatTu",
                table: "KiemKeChiTiet",
                column: "MaVatTu");

            migrationBuilder.CreateIndex(
                name: "IX_KiemKeVatTu_MaKho",
                table: "KiemKeVatTu",
                column: "MaKho");

            migrationBuilder.CreateIndex(
                name: "IX_KiemKeVatTu_MaNS",
                table: "KiemKeVatTu",
                column: "MaNS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KiemKeChiTiet");

            migrationBuilder.DropTable(
                name: "KiemKeVatTu");
        }
    }
}
