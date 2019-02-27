using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class KhoHangConfiguration : DbEntityConfiguration<KhoHang>
    {
        public override void Configure(EntityTypeBuilder<KhoHang> entity)
        {
            entity.HasKey(x => new { x.MaKho, x.MaPhieuNhap, x.MaVatTu });

            entity.HasOne(n => n.KhoVatTu)
             .WithMany(k => k.KhoHangs)
             .HasForeignKey(n => n.MaKho)
             .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.NhapVatTu)
                .WithMany(k => k.KhoHangs)
                .HasForeignKey(n => n.MaPhieuNhap)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.VaTu)
               .WithMany(k => k.KhoHangs)
               .HasForeignKey(n => n.MaVatTu)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}