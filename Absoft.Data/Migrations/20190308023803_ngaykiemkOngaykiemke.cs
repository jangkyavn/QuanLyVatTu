using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class ngaykiemkOngaykiemke : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NgayKiemK",
                table: "KiemKeVatTu",
                newName: "NgayKiemKe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NgayKiemKe",
                table: "KiemKeVatTu",
                newName: "NgayKiemK");
        }
    }
}
