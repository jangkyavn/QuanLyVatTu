using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class ChucNangConfiguration : DbEntityConfiguration<ChucNang>
    {
        public override void Configure(EntityTypeBuilder<ChucNang> entity)
        {
            entity.Property(p => p.MaChucNang).IsRequired().IsUnicode(false);
        }
    }
}
