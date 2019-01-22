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
    [Migration("20190122070521_ChangeName")]
    partial class ChangeName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Absoft.Data.Entities.KhoHang", b =>
                {
                    b.Property<int>("MaKho");

                    b.Property<int>("MaPhieuNhap");

                    b.Property<int>("MaVatTu");

                    b.Property<int>("SoLuongTon");

                    b.HasKey("MaKho", "MaPhieuNhap", "MaVatTu");

                    b.ToTable("KhoHang");
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

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("Gender");

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

            modelBuilder.Entity("Absoft.Data.Entities.NhapChiTiet", b =>
                {
                    b.Property<int>("MaPhieuNhap");

                    b.Property<int>("MaVatTu");

                    b.Property<decimal>("DonGia");

                    b.Property<string>("DotMua");

                    b.Property<string>("GhiChu");

                    b.Property<int>("MaHang");

                    b.Property<int>("MaNuoc");

                    b.Property<string>("Model");

                    b.Property<string>("NamSX");

                    b.Property<string>("NguonGoc");

                    b.Property<string>("PhanCap");

                    b.Property<string>("Seri");

                    b.Property<string>("SoDangKy");

                    b.Property<string>("SoKhung");

                    b.Property<int>("SoLuong");

                    b.Property<string>("SoMay");

                    b.HasKey("MaPhieuNhap", "MaVatTu");

                    b.ToTable("NhapChiTiet");
                });

            modelBuilder.Entity("Absoft.Data.Entities.NhapVatTu", b =>
                {
                    b.Property<int>("MaPhieuNhap")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChietKhau");

                    b.Property<string>("GhiChu");

                    b.Property<int>("MaHM");

                    b.Property<int>("MaKho");

                    b.Property<string>("NgayNhap");

                    b.Property<string>("NguoiNhap");

                    b.Property<int>("TongSoLuong");

                    b.Property<decimal>("TongSoTien");

                    b.HasKey("MaPhieuNhap");

                    b.ToTable("NhapVatTu");
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

            modelBuilder.Entity("Absoft.Data.Entities.XuatChiTiet", b =>
                {
                    b.Property<int>("MaPhieuXuat");

                    b.Property<int>("MaPhieuNhap");

                    b.Property<int>("MaVatTu");

                    b.Property<string>("GhiChu");

                    b.Property<int>("SoLuongXuat");

                    b.HasKey("MaPhieuXuat", "MaPhieuNhap", "MaVatTu");

                    b.ToTable("XuatChiTiet");
                });

            modelBuilder.Entity("Absoft.Data.Entities.XuatVatTu", b =>
                {
                    b.Property<int>("MaPhieuXuat")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GhiChu");

                    b.Property<int>("MaKho");

                    b.Property<int>("MaNS");

                    b.Property<string>("NgayNhap");

                    b.Property<int>("TongSoLuong");

                    b.Property<decimal>("TongSoTien");

                    b.HasKey("MaPhieuXuat");

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
