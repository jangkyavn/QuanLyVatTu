using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class VatTuConfiguration : DbEntityConfiguration<VatTu>
    {
        public override void Configure(EntityTypeBuilder<VatTu> entity)
        {
            entity.HasOne(v => v.LoaiVatTu)
                .WithMany(h => h.VatTus)
                .HasForeignKey(x => x.MaLoaiVatTu)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(v => v.DonViTinh)
                .WithMany(h => h.VatTus)
                .HasForeignKey(x => x.MaDVT)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
