using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class inserThanhLyVatTu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThanhLyVatTu",
                columns: table => new
                {
                    MaPhieuThanhLy = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SoPhieuThanhLy = table.Column<string>(nullable: true),
                    MaKho = table.Column<int>(nullable: false),
                    MaNS = table.Column<int>(nullable: false),
                    NgayThanhLy = table.Column<string>(nullable: true),
                    TongSoLuong = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhLyVatTu", x => x.MaPhieuThanhLy);
                    table.ForeignKey(
                        name: "FK_ThanhLyVatTu_KhoVatTu_MaKho",
                        column: x => x.MaKho,
                        principalTable: "KhoVatTu",
                        principalColumn: "MaKho",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThanhLyVatTu_NhanSu_MaNS",
                        column: x => x.MaNS,
                        principalTable: "NhanSu",
                        principalColumn: "MaNS",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ThanhLyChiTiet",
                columns: table => new
                {
                    MaPhieuThanhLy = table.Column<int>(nullable: false),
                    MaPhieuNhap = table.Column<int>(nullable: false),
                    MaVatTu = table.Column<int>(nullable: false),
                    SoLuongThanhLy = table.Column<int>(nullable: true),
                    DienGiai = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhLyChiTiet", x => new { x.MaPhieuThanhLy, x.MaPhieuNhap, x.MaVatTu });
                    table.ForeignKey(
                        name: "FK_ThanhLyChiTiet_NhapVatTu_MaPhieuNhap",
                        column: x => x.MaPhieuNhap,
                        principalTable: "NhapVatTu",
                        principalColumn: "MaPhieuNhap",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThanhLyChiTiet_ThanhLyVatTu_MaPhieuThanhLy",
                        column: x => x.MaPhieuThanhLy,
                        principalTable: "ThanhLyVatTu",
                        principalColumn: "MaPhieuThanhLy",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThanhLyChiTiet_VatTu_MaVatTu",
                        column: x => x.MaVatTu,
                        principalTable: "VatTu",
                        principalColumn: "MaVatTu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThanhLyChiTiet_MaPhieuNhap",
                table: "ThanhLyChiTiet",
                column: "MaPhieuNhap");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhLyChiTiet_MaVatTu",
                table: "ThanhLyChiTiet",
                column: "MaVatTu");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhLyVatTu_MaKho",
                table: "ThanhLyVatTu",
                column: "MaKho");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhLyVatTu_MaNS",
                table: "ThanhLyVatTu",
                column: "MaNS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThanhLyChiTiet");

            migrationBuilder.DropTable(
                name: "ThanhLyVatTu");
        }
    }
}
