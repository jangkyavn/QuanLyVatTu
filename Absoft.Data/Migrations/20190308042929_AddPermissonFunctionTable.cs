using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class AddPermissonFunctionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucNang",
                columns: table => new
                {
                    MaChucNang = table.Column<string>(unicode: false, nullable: false),
                    TenChucNang = table.Column<string>(nullable: true),
                    DuongDan = table.Column<string>(nullable: true),
                    BieuTuong = table.Column<string>(nullable: true),
                    ViTri = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucNang", x => x.MaChucNang);
                });

            migrationBuilder.CreateTable(
                name: "PhanQuyen",
                columns: table => new
                {
                    MaVaiTro = table.Column<Guid>(nullable: false),
                    MaChucNang = table.Column<string>(nullable: false),
                    Xem = table.Column<bool>(nullable: false),
                    Them = table.Column<bool>(nullable: false),
                    Sua = table.Column<bool>(nullable: false),
                    Xoa = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanQuyen", x => new { x.MaVaiTro, x.MaChucNang });
                    table.ForeignKey(
                        name: "FK_PhanQuyen_ChucNang_MaChucNang",
                        column: x => x.MaChucNang,
                        principalTable: "ChucNang",
                        principalColumn: "MaChucNang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhanQuyen_VaiTro_MaVaiTro",
                        column: x => x.MaVaiTro,
                        principalTable: "VaiTro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyen_MaChucNang",
                table: "PhanQuyen",
                column: "MaChucNang");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhanQuyen");

            migrationBuilder.DropTable(
                name: "ChucNang");
        }
    }
}
