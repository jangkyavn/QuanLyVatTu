using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class XuatChiTietConfiguration : DbEntityConfiguration<XuatChiTiet>
    {
        public override void Configure(EntityTypeBuilder<XuatChiTiet> entity)
        {
            entity.HasKey(x => new { x.MaPhieuXuat, x.MaPhieuNhap, x.MaVatTu });

            entity.HasOne(n => n.NhapVatTu)
             .WithMany(k => k.XuatChiTiets)
             .HasForeignKey(n => n.MaPhieuNhap)
             .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.XuatVatTu)
                .WithMany(k => k.XuatChiTiets)
                .HasForeignKey(n => n.MaPhieuXuat)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.VatTu)
               .WithMany(k => k.XuatChiTiets)
               .HasForeignKey(n => n.MaVatTu)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
