using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class ModifiedFunctionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasCreate",
                table: "ChucNang");

            migrationBuilder.DropColumn(
                name: "HasDelete",
                table: "ChucNang");

            migrationBuilder.DropColumn(
                name: "HasRead",
                table: "ChucNang");

            migrationBuilder.DropColumn(
                name: "HasUpdate",
                table: "ChucNang");

            migrationBuilder.CreateTable(
                name: "ChucNangHanhDong",
                columns: table => new
                {
                    MaChucNang = table.Column<string>(nullable: false),
                    MaHanhDong = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucNangHanhDong", x => new { x.MaChucNang, x.MaHanhDong });
                    table.ForeignKey(
                        name: "FK_ChucNangHanhDong_ChucNang_MaChucNang",
                        column: x => x.MaChucNang,
                        principalTable: "ChucNang",
                        principalColumn: "MaChucNang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChucNangHanhDong_HanhDong_MaHanhDong",
                        column: x => x.MaHanhDong,
                        principalTable: "HanhDong",
                        principalColumn: "MaHanhDong",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChucNangHanhDong_MaHanhDong",
                table: "ChucNangHanhDong",
                column: "MaHanhDong");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChucNangHanhDong");

            migrationBuilder.AddColumn<bool>(
                name: "HasCreate",
                table: "ChucNang",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDelete",
                table: "ChucNang",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasRead",
                table: "ChucNang",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasUpdate",
                table: "ChucNang",
                nullable: false,
                defaultValue: false);
        }
    }
}
