using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Absoft.Data.Configurations
{   
    public class KiemKeChiTietConfiguration : DbEntityConfiguration<KiemKeChiTiet>
    {
        public override void Configure(EntityTypeBuilder<KiemKeChiTiet> entity)
        {
            entity.HasKey(x => new { x.MaPhieuKiemKe, x.MaPhieuNhap, x.MaVatTu });

            entity.HasOne(n => n.KiemKeVatTu)
               .WithMany(k => k.KiemKeChiTiets)
               .HasForeignKey(n => n.MaPhieuKiemKe)
               .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.NhapVatTu)
                .WithMany(k => k.KiemKeChiTiets)
                .HasForeignKey(n => n.MaPhieuNhap)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.VatTu)
               .WithMany(k => k.KiemKeChiTiets)
               .HasForeignKey(n => n.MaVatTu)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
