using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class LoaiVatTuConfiguration : DbEntityConfiguration<LoaiVatTu>
    {
        public override void Configure(EntityTypeBuilder<LoaiVatTu> entity)
        {
            entity.HasOne(l => l.HangMucVatTu)
                .WithMany(h => h.LoaiVatTus)
                .HasForeignKey(x => x.MaHM)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
