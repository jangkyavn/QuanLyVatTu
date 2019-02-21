using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class NhapVatTuConfiguration : DbEntityConfiguration<NhapVatTu>
    {
        public override void Configure(EntityTypeBuilder<NhapVatTu> entity)
        {
            entity.HasOne(n => n.KhoVatTu)
                .WithMany(k => k.NhapVatTus)
                .HasForeignKey(n => n.MaKho)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.HangMucVatTu)
                .WithMany(h => h.NhapVatTus)
                .HasForeignKey(n => n.MaHM)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
