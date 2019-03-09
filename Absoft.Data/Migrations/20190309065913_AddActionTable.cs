using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class AddActionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PhanQuyen",
                table: "PhanQuyen");

            migrationBuilder.DropColumn(
                name: "Sua",
                table: "PhanQuyen");

            migrationBuilder.DropColumn(
                name: "Them",
                table: "PhanQuyen");

            migrationBuilder.DropColumn(
                name: "Xem",
                table: "PhanQuyen");

            migrationBuilder.DropColumn(
                name: "Xoa",
                table: "PhanQuyen");

            migrationBuilder.AddColumn<string>(
                name: "MaHanhDong",
                table: "PhanQuyen",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhanQuyen",
                table: "PhanQuyen",
                columns: new[] { "MaVaiTro", "MaChucNang", "MaHanhDong" });

            migrationBuilder.CreateTable(
                name: "HanhDong",
                columns: table => new
                {
                    MaHanhDong = table.Column<string>(unicode: false, nullable: false),
                    TenHanhDong = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HanhDong", x => x.MaHanhDong);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyen_MaHanhDong",
                table: "PhanQuyen",
                column: "MaHanhDong");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanQuyen_HanhDong_MaHanhDong",
                table: "PhanQuyen",
                column: "MaHanhDong",
                principalTable: "HanhDong",
                principalColumn: "MaHanhDong",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhanQuyen_HanhDong_MaHanhDong",
                table: "PhanQuyen");

            migrationBuilder.DropTable(
                name: "HanhDong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhanQuyen",
                table: "PhanQuyen");

            migrationBuilder.DropIndex(
                name: "IX_PhanQuyen_MaHanhDong",
                table: "PhanQuyen");

            migrationBuilder.DropColumn(
                name: "MaHanhDong",
                table: "PhanQuyen");

            migrationBuilder.AddColumn<bool>(
                name: "Sua",
                table: "PhanQuyen",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Them",
                table: "PhanQuyen",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Xem",
                table: "PhanQuyen",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Xoa",
                table: "PhanQuyen",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhanQuyen",
                table: "PhanQuyen",
                columns: new[] { "MaVaiTro", "MaChucNang" });
        }
    }
}
