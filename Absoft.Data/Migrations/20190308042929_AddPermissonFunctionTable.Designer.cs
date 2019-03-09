﻿// <auto-generated />
using System;
using Absoft.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Absoft.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190308042929_AddPermissonFunctionTable")]
    partial class AddPermissonFunctionTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Absoft.Data.Entities.ChucNang", b =>
                {
                    b.Property<string>("MaChucNang")
                        .ValueGeneratedOnAdd()
                        .IsUnicode(false);

                    b.Property<string>("BieuTuong");

                    b.Property<string>("DuongDan");

                    b.Property<bool>("Status");

                    b.Property<string>("TenChucNang");

                    b.Property<int>("ViTri");

                    b.HasKey("MaChucNang");

                    b.ToTable("ChucNang");
                });

            modelBuilder.Entity("Absoft.Data.Entities.DonViTinh", b =>
                {
                    b.Property<int>("MaDVT")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Status");

                    b.Property<string>("TenDVT");

                    b.HasKey("MaDVT");

                    b.ToTable("DonViTinh");
                });

            modelBuilder.Entity("Absoft.Data.Entities.HangMucVatTu", b =>
                {
                    b.Property<int>("MaHM")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GhiChu");

                    b.Property<bool>("Status");

                    b.Property<string>("TenHM");

                    b.HasKey("MaHM");

                    b.ToTable("HangMucVatTu");
                });

            modelBuilder.Entity("Absoft.Data.Entities.HangSanXuat", b =>
                {
                    b.Property<int>("MaHang")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GhiChu");

                    b.Property<bool>("Status");

                    b.Property<string>("TenHang");

                    b.HasKey("MaHang");

                    b.ToTable("HangSanXuat");
                });

            modelBuilder.Entity("Absoft.Data.Entities.KhoHang", b =>
                {
                    b.Property<int>("MaKho");

                    b.Property<int>("MaPhieuNhap");

                    b.Property<int>("MaVatTu");

                    b.Property<int?>("SoLuongTon");

                    b.Property<bool>("Status");

                    b.HasKey("MaKho", "MaPhieuNhap", "MaVatTu");

                    b.HasIndex("MaPhieuNhap");

                    b.HasIndex("MaVatTu");

                    b.ToTable("KhoHang");
                });

            modelBuilder.Entity("Absoft.Data.Entities.KhoVatTu", b =>
                {
                    b.Property<int>("MaKho")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaChi");

                    b.Property<string>("DienThoai");

                    b.Property<string>("GhiChu");

                    b.Property<bool>("Status");

                    b.Property<string>("TenKho");

                    b.HasKey("MaKho");

                    b.ToTable("KhoVatTu");
                });

            modelBuilder.Entity("Absoft.Data.Entities.KiemKeChiTiet", b =>
                {
                    b.Property<int>("MaPhieuKiemKe");

                    b.Property<int>("MaPhieuNhap");

                    b.Property<int>("MaVatTu");

                    b.Property<string>("GhiChu");

                    b.Property<int?>("SoLuongTheoDoi");

                    b.Property<int?>("SoLuongThucTon");

                    b.Property<bool>("Status");

                    b.HasKey("MaPhieuKiemKe", "MaPhieuNhap", "MaVatTu");

                    b.HasIndex("MaPhieuNhap");

                    b.HasIndex("MaVatTu");

                    b.ToTable("KiemKeChiTiet");
                });

            modelBuilder.Entity("Absoft.Data.Entities.KiemKeVatTu", b =>
                {
                    b.Property<int>("MaPhieuKiemKe")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaKho");

                    b.Property<int>("MaNS");

                    b.Property<string>("NgayKiemKe");

                    b.Property<string>("SoPhieuKiemKe");

                    b.Property<bool>("Status");

                    b.Property<int?>("TongTheoDoi");

                    b.Property<int?>("TongThucTon");

                    b.HasKey("MaPhieuKiemKe");

                    b.HasIndex("MaKho");

                    b.HasIndex("MaNS");

                    b.ToTable("KiemKeVatTu");
                });

            modelBuilder.Entity("Absoft.Data.Entities.LoaiVatTu", b =>
                {
                    b.Property<int>("MaLoaiVatTu")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GhiChu");

                    b.Property<int>("MaHM");

                    b.Property<bool>("Status");

                    b.Property<string>("TenLoai");

                    b.HasKey("MaLoaiVatTu");

                    b.HasIndex("MaHM");

                    b.ToTable("LoaiVatTu");
                });

            modelBuilder.Entity("Absoft.Data.Entities.NguoiDung", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<string>("Avatar")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool?>("Gender");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("Status");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("NguoiDung");
                });

            modelBuilder.Entity("Absoft.Data.Entities.NguonCungCap", b =>
                {
                    b.Property<int>("MaNguon")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GhiChu");

                    b.Property<bool>("Status");

                    b.Property<string>("TenNguon");

                    b.HasKey("MaNguon");

                    b.ToTable("NguonCungCap");
                });

            modelBuilder.Entity("Absoft.Data.Entities.NhanSu", b =>
                {
                    b.Property<int>("MaNS")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BanThan");

                    b.Property<string>("CapBacSHSQ");

                    b.Property<string>("ChucVu");

                    b.Property<string>("DanToc");

                    b.Property<string>("Dang");

                    b.Property<string>("GhiChu");

                    b.Property<string>("HoTen");

                    b.Property<string>("NgayNhapNguTuyenDung");

                    b.Property<string>("NgaySinh");

                    b.Property<string>("QuaTrinhHocTap");

                    b.Property<string>("QueQuan");

                    b.Property<bool>("Status");

                    b.Property<string>("ThanNhan");

                    b.Property<string>("TonGiao");

                    b.Property<string>("XuatThan");

                    b.HasKey("MaNS");

                    b.ToTable("NhanSu");
                });

            modelBuilder.Entity("Absoft.Data.Entities.NhapChiTiet", b =>
                {
                    b.Property<int>("MaPhieuNhap");

                    b.Property<int>("MaVatTu");

                    b.Property<string>("BietDuoc");

                    b.Property<decimal?>("DonGia");

                    b.Property<string>("DotMua");

                    b.Property<string>("GhiChu");

                    b.Property<string>("HanDung");

                    b.Property<int?>("MaHang");

                    b.Property<int?>("MaNguon");

                    b.Property<int?>("MaNuoc");

                    b.Property<string>("Model");

                    b.Property<string>("NamSX");

                    b.Property<string>("PhanCap");

                    b.Property<string>("Seri");

                    b.Property<string>("SoDangKy");

                    b.Property<string>("SoKhung");

                    b.Property<string>("SoLo");

                    b.Property<int?>("SoLuong");

                    b.Property<string>("SoMay");

                    b.Property<bool>("Status");

                    b.HasKey("MaPhieuNhap", "MaVatTu");

                    b.HasIndex("MaHang");

                    b.HasIndex("MaNguon");

                    b.HasIndex("MaNuoc");

                    b.HasIndex("MaVatTu");

                    b.ToTable("NhapChiTiet");
                });

            modelBuilder.Entity("Absoft.Data.Entities.NhapVatTu", b =>
                {
                    b.Property<int>("MaPhieuNhap")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("ChietKhau");

                    b.Property<string>("GhiChu");

                    b.Property<int>("MaHM");

                    b.Property<int>("MaKho");

                    b.Property<string>("NgayNhap");

                    b.Property<string>("NguoiNhap");

                    b.Property<string>("SoPhieuNhap");

                    b.Property<bool>("Status");

                    b.Property<int?>("TongSoLuong");

                    b.Property<decimal?>("TongSoTien");

                    b.HasKey("MaPhieuNhap");

                    b.HasIndex("MaHM");

                    b.HasIndex("MaKho");

                    b.ToTable("NhapVatTu");
                });

            modelBuilder.Entity("Absoft.Data.Entities.NuocSanXuat", b =>
                {
                    b.Property<int>("MaNuoc")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Status");

                    b.Property<string>("TenNuoc");

                    b.HasKey("MaNuoc");

                    b.ToTable("NuocSanXuat");
                });

            modelBuilder.Entity("Absoft.Data.Entities.PhanQuyen", b =>
                {
                    b.Property<Guid>("MaVaiTro");

                    b.Property<string>("MaChucNang");

                    b.Property<bool>("Sua");

                    b.Property<bool>("Them");

                    b.Property<bool>("Xem");

                    b.Property<bool>("Xoa");

                    b.HasKey("MaVaiTro", "MaChucNang");

                    b.HasIndex("MaChucNang");

                    b.ToTable("PhanQuyen");
                });

            modelBuilder.Entity("Absoft.Data.Entities.ThanhLyChiTiet", b =>
                {
                    b.Property<int>("MaPhieuThanhLy");

                    b.Property<int>("MaPhieuNhap");

                    b.Property<int>("MaVatTu");

                    b.Property<string>("DienGiai");

                    b.Property<string>("GhiChu");

                    b.Property<int?>("SoLuongThanhLy");

                    b.Property<bool>("Status");

                    b.HasKey("MaPhieuThanhLy", "MaPhieuNhap", "MaVatTu");

                    b.HasIndex("MaPhieuNhap");

                    b.HasIndex("MaVatTu");

                    b.ToTable("ThanhLyChiTiet");
                });

            modelBuilder.Entity("Absoft.Data.Entities.ThanhLyVatTu", b =>
                {
                    b.Property<int>("MaPhieuThanhLy")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaKho");

                    b.Property<int>("MaNS");

                    b.Property<string>("NgayThanhLy");

                    b.Property<string>("SoPhieuThanhLy");

                    b.Property<bool>("Status");

                    b.Property<int?>("TongSoLuong");

                    b.HasKey("MaPhieuThanhLy");

                    b.HasIndex("MaKho");

                    b.HasIndex("MaNS");

                    b.ToTable("ThanhLyVatTu");
                });

            modelBuilder.Entity("Absoft.Data.Entities.VaiTro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("VaiTro");
                });

            modelBuilder.Entity("Absoft.Data.Entities.VaiTroNguoiDung", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("VaiTroNguoiDung");
                });

            modelBuilder.Entity("Absoft.Data.Entities.VatTu", b =>
                {
                    b.Property<int>("MaVatTu")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GhiChu");

                    b.Property<int?>("MaDVT");

                    b.Property<int>("MaLoaiVatTu");

                    b.Property<bool>("Status");

                    b.Property<string>("TenVT");

                    b.HasKey("MaVatTu");

                    b.HasIndex("MaDVT");

                    b.HasIndex("MaLoaiVatTu");

                    b.ToTable("VatTu");
                });

            modelBuilder.Entity("Absoft.Data.Entities.XuatChiTiet", b =>
                {
                    b.Property<int>("MaPhieuXuat");

                    b.Property<int>("MaPhieuNhap");

                    b.Property<int>("MaVatTu");

                    b.Property<decimal?>("DonGia");

                    b.Property<string>("GhiChu");

                    b.Property<int?>("SoLuongXuat");

                    b.Property<bool>("Status");

                    b.HasKey("MaPhieuXuat", "MaPhieuNhap", "MaVatTu");

                    b.HasIndex("MaPhieuNhap");

                    b.HasIndex("MaVatTu");

                    b.ToTable("XuatChiTiet");
                });

            modelBuilder.Entity("Absoft.Data.Entities.XuatVatTu", b =>
                {
                    b.Property<int>("MaPhieuXuat")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("ChietKhau");

                    b.Property<string>("GhiChu");

                    b.Property<int>("MaKho");

                    b.Property<int>("MaNS");

                    b.Property<string>("NgayNhap");

                    b.Property<string>("SoPhieuXuat");

                    b.Property<bool>("Status");

                    b.Property<int?>("TongSoLuong");

                    b.Property<decimal?>("TongSoTien");

                    b.HasKey("MaPhieuXuat");

                    b.HasIndex("MaKho");

                    b.HasIndex("MaNS");

                    b.ToTable("XuatVatTu");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Absoft.Data.Entities.KhoHang", b =>
                {
                    b.HasOne("Absoft.Data.Entities.KhoVatTu", "KhoVatTu")
                        .WithMany("KhoHangs")
                        .HasForeignKey("MaKho")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.NhapVatTu", "NhapVatTu")
                        .WithMany("KhoHangs")
                        .HasForeignKey("MaPhieuNhap")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.VatTu", "VaTu")
                        .WithMany("KhoHangs")
                        .HasForeignKey("MaVatTu")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Absoft.Data.Entities.KiemKeChiTiet", b =>
                {
                    b.HasOne("Absoft.Data.Entities.KiemKeVatTu", "KiemKeVatTu")
                        .WithMany("KiemKeChiTiets")
                        .HasForeignKey("MaPhieuKiemKe")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.NhapVatTu", "NhapVatTu")
                        .WithMany("KiemKeChiTiets")
                        .HasForeignKey("MaPhieuNhap")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.VatTu", "VatTu")
                        .WithMany("KiemKeChiTiets")
                        .HasForeignKey("MaVatTu")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Absoft.Data.Entities.KiemKeVatTu", b =>
                {
                    b.HasOne("Absoft.Data.Entities.KhoVatTu", "KhoVatTu")
                        .WithMany("KiemKeVatTus")
                        .HasForeignKey("MaKho")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.NhanSu", "NhanSu")
                        .WithMany("KiemKeVatTus")
                        .HasForeignKey("MaNS")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Absoft.Data.Entities.LoaiVatTu", b =>
                {
                    b.HasOne("Absoft.Data.Entities.HangMucVatTu", "HangMucVatTu")
                        .WithMany("LoaiVatTus")
                        .HasForeignKey("MaHM")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Absoft.Data.Entities.NhapChiTiet", b =>
                {
                    b.HasOne("Absoft.Data.Entities.HangSanXuat", "HangSanXuat")
                        .WithMany("NhapChiTiets")
                        .HasForeignKey("MaHang")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.NguonCungCap", "NguonCungCap")
                        .WithMany("NhapChiTiets")
                        .HasForeignKey("MaNguon")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.NuocSanXuat", "NuocSanXuat")
                        .WithMany("NhapChiTiets")
                        .HasForeignKey("MaNuoc")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.NhapVatTu", "NhapVatTu")
                        .WithMany("NhapChiTiets")
                        .HasForeignKey("MaPhieuNhap")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.VatTu", "VatTu")
                        .WithMany("NhapChiTiets")
                        .HasForeignKey("MaVatTu")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Absoft.Data.Entities.NhapVatTu", b =>
                {
                    b.HasOne("Absoft.Data.Entities.HangMucVatTu", "HangMucVatTu")
                        .WithMany("NhapVatTus")
                        .HasForeignKey("MaHM")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.KhoVatTu", "KhoVatTu")
                        .WithMany("NhapVatTus")
                        .HasForeignKey("MaKho")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Absoft.Data.Entities.PhanQuyen", b =>
                {
                    b.HasOne("Absoft.Data.Entities.ChucNang", "ChucNang")
                        .WithMany("PhanQuyens")
                        .HasForeignKey("MaChucNang")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.VaiTro", "VaiTro")
                        .WithMany("PhanQuyens")
                        .HasForeignKey("MaVaiTro")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Absoft.Data.Entities.ThanhLyChiTiet", b =>
                {
                    b.HasOne("Absoft.Data.Entities.NhapVatTu", "NhapVatTu")
                        .WithMany("ThanhLyChiTiets")
                        .HasForeignKey("MaPhieuNhap")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.ThanhLyVatTu", "ThanhLyVatTu")
                        .WithMany("ThanhLyChiTiets")
                        .HasForeignKey("MaPhieuThanhLy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.VatTu", "VatTu")
                        .WithMany("ThanhLyChiTiets")
                        .HasForeignKey("MaVatTu")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Absoft.Data.Entities.ThanhLyVatTu", b =>
                {
                    b.HasOne("Absoft.Data.Entities.KhoVatTu", "KhoVatTu")
                        .WithMany("ThanhLyVatTus")
                        .HasForeignKey("MaKho")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.NhanSu", "NhanSu")
                        .WithMany("ThanhLyVatTus")
                        .HasForeignKey("MaNS")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Absoft.Data.Entities.VaiTroNguoiDung", b =>
                {
                    b.HasOne("Absoft.Data.Entities.VaiTro", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Absoft.Data.Entities.NguoiDung", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Absoft.Data.Entities.VatTu", b =>
                {
                    b.HasOne("Absoft.Data.Entities.DonViTinh", "DonViTinh")
                        .WithMany("VatTus")
                        .HasForeignKey("MaDVT")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.LoaiVatTu", "LoaiVatTu")
                        .WithMany("VatTus")
                        .HasForeignKey("MaLoaiVatTu")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Absoft.Data.Entities.XuatChiTiet", b =>
                {
                    b.HasOne("Absoft.Data.Entities.NhapVatTu", "NhapVatTu")
                        .WithMany("XuatChiTiets")
                        .HasForeignKey("MaPhieuNhap")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.XuatVatTu", "XuatVatTu")
                        .WithMany("XuatChiTiets")
                        .HasForeignKey("MaPhieuXuat")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.VatTu", "VatTu")
                        .WithMany("XuatChiTiets")
                        .HasForeignKey("MaVatTu")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Absoft.Data.Entities.XuatVatTu", b =>
                {
                    b.HasOne("Absoft.Data.Entities.KhoVatTu", "KhoVatTu")
                        .WithMany("XuatVatTus")
                        .HasForeignKey("MaKho")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Absoft.Data.Entities.NhanSu", "NhanSu")
                        .WithMany("XuatVatTus")
                        .HasForeignKey("MaNS")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Absoft.Data.Entities.VaiTro")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Absoft.Data.Entities.NguoiDung")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Absoft.Data.Entities.NguoiDung")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Absoft.Data.Entities.NguoiDung")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
