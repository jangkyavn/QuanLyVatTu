using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Absoft.Data.Migrations
{
    public partial class CreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonViTinh",
                columns: table => new
                {
                    MaDVT = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenDVT = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonViTinh", x => x.MaDVT);
                });

            migrationBuilder.CreateTable(
                name: "HangMucVatTu",
                columns: table => new
                {
                    MaHM = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenHM = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangMucVatTu", x => x.MaHM);
                });

            migrationBuilder.CreateTable(
                name: "HangSanXuat",
                columns: table => new
                {
                    MaHang = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenHang = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangSanXuat", x => x.MaHang);
                });

            migrationBuilder.CreateTable(
                name: "KhoHang",
                columns: table => new
                {
                    MaKho = table.Column<int>(nullable: false),
                    MaPhieuNhap = table.Column<int>(nullable: false),
                    MaVatTu = table.Column<int>(nullable: false),
                    SoLuongTon = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoHang", x => new { x.MaKho, x.MaPhieuNhap, x.MaVatTu });
                });

            migrationBuilder.CreateTable(
                name: "KhoVatTu",
                columns: table => new
                {
                    MaKho = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenKho = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    DienThoai = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoVatTu", x => x.MaKho);
                });

            migrationBuilder.CreateTable(
                name: "LoaiVatTu",
                columns: table => new
                {
                    MaLoaiVatTu = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaHM = table.Column<int>(nullable: false),
                    TenLoai = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiVatTu", x => x.MaLoaiVatTu);
                });

            migrationBuilder.CreateTable(
                name: "NuocSanXuat",
                columns: table => new
                {
                    MaNuoc = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenNuoc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NuocSanXuat", x => x.MaNuoc);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<bool>(nullable: false),
                    Avatar = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NguonCungCap",
                columns: table => new
                {
                    MaNguon = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenNguon = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguonCungCap", x => x.MaNguon);
                });

            migrationBuilder.CreateTable(
                name: "NhanSu",
                columns: table => new
                {
                    MaNS = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HoTen = table.Column<string>(nullable: true),
                    NgaySinh = table.Column<string>(nullable: true),
                    QueQuan = table.Column<string>(nullable: true),
                    DanToc = table.Column<string>(nullable: true),
                    TonGiao = table.Column<string>(nullable: true),
                    CapBacSHSQ = table.Column<string>(nullable: true),
                    ChucVu = table.Column<string>(nullable: true),
                    NgayNhapNguTuyenDung = table.Column<string>(nullable: true),
                    XuatThan = table.Column<string>(nullable: true),
                    BanThan = table.Column<string>(nullable: true),
                    QuaTrinhHocTap = table.Column<string>(nullable: true),
                    ThanNhan = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanSu", x => x.MaNS);
                });

            migrationBuilder.CreateTable(
                name: "NhapChiTiet",
                columns: table => new
                {
                    MaPhieuNhap = table.Column<int>(nullable: false),
                    MaVatTu = table.Column<int>(nullable: false),
                    MaNuoc = table.Column<int>(nullable: true),
                    MaHang = table.Column<int>(nullable: true),
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
                    table.PrimaryKey("PK_NhapChiTiet", x => new { x.MaPhieuNhap, x.MaVatTu });
                });

            migrationBuilder.CreateTable(
                name: "NhapVatTu",
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
                    GhiChu = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhapVatTu", x => x.MaPhieuNhap);
                });

            migrationBuilder.CreateTable(
                name: "VaiTro",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VatTu",
                columns: table => new
                {
                    MaVatTu = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaLoaiVatTu = table.Column<int>(nullable: false),
                    MaDVT = table.Column<int>(nullable: false),
                    TenVT = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatTu", x => x.MaVatTu);
                });

            migrationBuilder.CreateTable(
                name: "XuatChiTiet",
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
                    table.PrimaryKey("PK_XuatChiTiet", x => new { x.MaPhieuXuat, x.MaPhieuNhap, x.MaVatTu });
                });

            migrationBuilder.CreateTable(
                name: "XuatVatTu",
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
                    table.PrimaryKey("PK_XuatVatTu", x => x.MaPhieuXuat);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_NguoiDung_UserId",
                        column: x => x.UserId,
                        principalTable: "NguoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_NguoiDung_UserId",
                        column: x => x.UserId,
                        principalTable: "NguoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_NguoiDung_UserId",
                        column: x => x.UserId,
                        principalTable: "NguoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_VaiTro_RoleId",
                        column: x => x.RoleId,
                        principalTable: "VaiTro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaiTroNguoiDung",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTroNguoiDung", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_VaiTroNguoiDung_VaiTro_RoleId",
                        column: x => x.RoleId,
                        principalTable: "VaiTro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaiTroNguoiDung_NguoiDung_UserId",
                        column: x => x.UserId,
                        principalTable: "NguoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "NguoiDung",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "NguoiDung",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "VaiTro",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VaiTroNguoiDung_RoleId",
                table: "VaiTroNguoiDung",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DonViTinh");

            migrationBuilder.DropTable(
                name: "HangMucVatTu");

            migrationBuilder.DropTable(
                name: "HangSanXuat");

            migrationBuilder.DropTable(
                name: "KhoHang");

            migrationBuilder.DropTable(
                name: "KhoVatTu");

            migrationBuilder.DropTable(
                name: "LoaiVatTu");

            migrationBuilder.DropTable(
                name: "NuocSanXuat");

            migrationBuilder.DropTable(
                name: "NguonCungCap");

            migrationBuilder.DropTable(
                name: "NhanSu");

            migrationBuilder.DropTable(
                name: "NhapChiTiet");

            migrationBuilder.DropTable(
                name: "NhapVatTu");

            migrationBuilder.DropTable(
                name: "VaiTroNguoiDung");

            migrationBuilder.DropTable(
                name: "VatTu");

            migrationBuilder.DropTable(
                name: "XuatChiTiet");

            migrationBuilder.DropTable(
                name: "XuatVatTu");

            migrationBuilder.DropTable(
                name: "VaiTro");

            migrationBuilder.DropTable(
                name: "NguoiDung");
        }
    }
}
