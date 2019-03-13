using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class insertsoluongkiemke : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoLuongKiemKe",
                table: "KiemKeChiTiet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoLuongKiemKe",
                table: "KiemKeChiTiet");
        }
    }
}
