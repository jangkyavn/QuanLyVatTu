using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Absoft.Data.Configurations
{
    public class ThanhLyChiTietConfiguration : DbEntityConfiguration<ThanhLyChiTiet>
    {
        public override void Configure(EntityTypeBuilder<ThanhLyChiTiet> entity)
        {
            entity.HasKey(x => new { x.MaPhieuThanhLy, x.MaPhieuNhap, x.MaVatTu });

            entity.HasOne(n => n.ThanhLyVatTu)
               .WithMany(k => k.ThanhLyChiTiets)
               .HasForeignKey(n => n.MaPhieuThanhLy)
               .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.NhapVatTu)
                .WithMany(k => k.ThanhLyChiTiets)
                .HasForeignKey(n => n.MaPhieuNhap)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.VatTu)
               .WithMany(k => k.ThanhLyChiTiets)
               .HasForeignKey(n => n.MaVatTu)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
