using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class KhoHangConfiguration : DbEntityConfiguration<KhoHang>
    {
        public override void Configure(EntityTypeBuilder<KhoHang> entity)
        {

            entity.HasKey(x => new { x.MaKho, x.MaPhieuNhap, x.MaVatTu });
        }
    }
}