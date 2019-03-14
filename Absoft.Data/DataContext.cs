using Absoft.Data.Configurations;
using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Absoft.Data
{
    public class DataContext : IdentityDbContext<NguoiDung, VaiTro, Guid, IdentityUserClaim<Guid>,
        VaiTroNguoiDung, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DataContext(DbContextOptions options) : base(options) { }


        public DbSet<DonViTinh> DonViTinhs { get; set; }
        public DbSet<HangMucVatTu> HangMucVatTus { get; set; }
        public DbSet<HangSanXuat> HangSanXuats { get; set; }
        public DbSet<KhoVatTu> KhoVatTus { get; set; }
        public DbSet<LoaiVatTu> LoaiVatTus { get; set; }
        public DbSet<NguonCungCap> NguonCungCaps { get; set; }
        public DbSet<NhanSu> NhanSus { get; set; }
        public DbSet<NuocSanXuat> NuocSanXuats { get; set; }
        public DbSet<VatTu> VatTus { get; set; }
        public DbSet<NhapVatTu> NhapVatTus { get; set; }
        public DbSet<NhapChiTiet> NhapChiTiets { get; set; }
        public DbSet<KhoHang> KhoHangs { get; set; }
        public DbSet<XuatVatTu> XuatVatTus { get; set; }
        public DbSet<XuatChiTiet> XuatChiTiets { get; set; }
        public DbSet<ThanhLyVatTu> ThanhLyVatTus { get; set; }
        public DbSet<ThanhLyChiTiet> ThanhLyChiTiets { get; set; }
        public DbSet<KiemKeVatTu> KiemKeVatTus { get; set; }
        public DbSet<KiemKeChiTiet> KiemKeChiTiets { get; set; }
        public DbSet<ChucNang> ChucNangs { get; set; }
        public DbSet<PhanQuyen> PhanQuyens { get; set; }
        public DbSet<HanhDong> HanhDongs { get; set; }
        public DbSet<ChucNangHanhDong> ChucNangHanhDongs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddConfiguration(new KhoHangConfiguration());
            modelBuilder.AddConfiguration(new LoaiVatTuConfiguration());
            modelBuilder.AddConfiguration(new NguoiDungConfiguration());
            modelBuilder.AddConfiguration(new NhapChiTietConfiguration());
            modelBuilder.AddConfiguration(new NhapVatTuConfiguration());
            modelBuilder.AddConfiguration(new VaiTroConfiguration());
            modelBuilder.AddConfiguration(new VaiTroNguoiDungConfiguration());
            modelBuilder.AddConfiguration(new VatTuConfiguration());
            modelBuilder.AddConfiguration(new XuatChiTietConfiguration());
            modelBuilder.AddConfiguration(new XuatVatTuConfiguration());
            modelBuilder.AddConfiguration(new ThanhLyVatTuConfiguration());
            modelBuilder.AddConfiguration(new ThanhLyChiTietConfiguration());
            modelBuilder.AddConfiguration(new KiemKeVatTuConfiguration());
            modelBuilder.AddConfiguration(new KiemKeChiTietConfiguration());
            modelBuilder.AddConfiguration(new ChucNangConfiguration());
            modelBuilder.AddConfiguration(new PhanQuyenConfiguration());
            modelBuilder.AddConfiguration(new HanhDongConfiguration());
            modelBuilder.AddConfiguration(new ChucNangHanhDongConfiguration());
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
        {
            public DataContext CreateDbContext(string[] args)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
                var builder = new DbContextOptionsBuilder<DataContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);
                return new DataContext(builder.Options);
            }
        }
    }
}
