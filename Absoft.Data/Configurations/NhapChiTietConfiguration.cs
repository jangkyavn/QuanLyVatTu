using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class NhapChiTietConfiguration : DbEntityConfiguration<NhapChiTiet>
    {
        public override void Configure(EntityTypeBuilder<NhapChiTiet> entity)
        {

            entity.HasKey(x => new { x.MaPhieuNhap, x.MaVatTu });
        }
    }
}
