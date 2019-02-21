using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class ChangeTypeChietKhau : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ChietKhau",
                table: "XuatVatTu",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ChietKhau",
                table: "NhapVatTu",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ChietKhau",
                table: "XuatVatTu",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ChietKhau",
                table: "NhapVatTu",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
