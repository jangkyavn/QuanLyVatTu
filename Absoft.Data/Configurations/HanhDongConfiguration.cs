using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class HanhDongConfiguration : DbEntityConfiguration<HanhDong>
    {
        public override void Configure(EntityTypeBuilder<HanhDong> entity)
        {
            entity.Property(p => p.MaHanhDong).IsRequired().IsUnicode(false);
        }
    }
}
