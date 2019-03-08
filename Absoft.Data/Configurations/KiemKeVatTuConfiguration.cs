using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Absoft.Data.Configurations
{   
    public class KiemKeVatTuConfiguration : DbEntityConfiguration<KiemKeVatTu>
    {
        public override void Configure(EntityTypeBuilder<KiemKeVatTu> entity)
        {
            entity.HasOne(n => n.KhoVatTu)
                .WithMany(k => k.KiemKeVatTus)
                .HasForeignKey(n => n.MaKho)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.NhanSu)
                .WithMany(h => h.KiemKeVatTus)
                .HasForeignKey(n => n.MaNS)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
