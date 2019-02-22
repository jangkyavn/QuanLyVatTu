using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class NhapChiTietConfiguration : DbEntityConfiguration<NhapChiTiet>
    {
        public override void Configure(EntityTypeBuilder<NhapChiTiet> entity)
        {
            entity.HasKey(x => new { x.MaPhieuNhap, x.MaVatTu });

            entity.HasOne(n => n.NuocSanXuat)
                .WithMany(k => k.NhapChiTiets)
                .HasForeignKey(n => n.MaNuoc)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.HangSanXuat)
                .WithMany(h => h.NhapChiTiets)
                .HasForeignKey(n => n.MaHang)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.NguonCungCap)
                .WithMany(h => h.NhapChiTiets)
                .HasForeignKey(n => n.MaNguon)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
