using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class XuatVatTuConfiguration : DbEntityConfiguration<XuatVatTu>
    {
        public override void Configure(EntityTypeBuilder<XuatVatTu> entity)
        {
            entity.HasOne(n => n.KhoVatTu)
                .WithMany(k => k.XuatVatTus)
                .HasForeignKey(n => n.MaKho)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.NhanSu)
                .WithMany(h => h.XuatVatTus)
                .HasForeignKey(n => n.MaNS)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
