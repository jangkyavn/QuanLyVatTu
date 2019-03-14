using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class inserttongkiemke : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TongKiemKe",
                table: "KiemKeVatTu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TongKiemKe",
                table: "KiemKeVatTu");
        }
    }
}
