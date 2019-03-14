using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class ChucNangHanhDongConfiguration : DbEntityConfiguration<ChucNangHanhDong>
    {
        public override void Configure(EntityTypeBuilder<ChucNangHanhDong> entity)
        {
            entity.HasKey(x => new { x.MaChucNang, x.MaHanhDong });

            entity.HasOne(n => n.ChucNang)
             .WithMany(k => k.ChucNangHanhDongs)
             .HasForeignKey(n => n.MaChucNang)
             .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.HanhDong)
                .WithMany(k => k.ChucNangHanhDongs)
                .HasForeignKey(n => n.MaHanhDong)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
