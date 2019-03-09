using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class PhanQuyenConfiguration : DbEntityConfiguration<PhanQuyen>
    {
        public override void Configure(EntityTypeBuilder<PhanQuyen> entity)
        {
            entity.HasKey(x => new { x.MaVaiTro, x.MaChucNang, x.MaHanhDong });

            entity.HasOne(n => n.VaiTro)
             .WithMany(k => k.PhanQuyens)
             .HasForeignKey(n => n.MaVaiTro)
             .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.ChucNang)
                .WithMany(k => k.PhanQuyens)
                .HasForeignKey(n => n.MaChucNang)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.HanhDong)
                .WithMany(k => k.PhanQuyens)
                .HasForeignKey(n => n.MaHanhDong)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
