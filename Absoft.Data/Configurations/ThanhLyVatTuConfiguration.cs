using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Absoft.Data.Configurations
{
    public class ThanhLyVatTuConfiguration : DbEntityConfiguration<ThanhLyVatTu>
    {
        public override void Configure(EntityTypeBuilder<ThanhLyVatTu> entity)
        {
            entity.HasOne(n => n.KhoVatTu)
                .WithMany(k => k.ThanhLyVatTus)
                .HasForeignKey(n => n.MaKho)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.NhanSu)
                .WithMany(h => h.ThanhLyVatTus)
                .HasForeignKey(n => n.MaNS)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
