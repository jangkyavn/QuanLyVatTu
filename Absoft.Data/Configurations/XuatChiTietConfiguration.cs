using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class XuatChiTietConfiguration : DbEntityConfiguration<XuatChiTiet>
    {
        public override void Configure(EntityTypeBuilder<XuatChiTiet> entity)
        {
            entity.HasKey(x => new { x.MaPhieuXuat, x.MaPhieuNhap, x.MaVatTu });
        }
    }
}
