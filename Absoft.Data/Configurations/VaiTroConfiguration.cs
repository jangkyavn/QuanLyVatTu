using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class VaiTroConfiguration : DbEntityConfiguration<VaiTro>
    {
        public override void Configure(EntityTypeBuilder<VaiTro> entity)
        {
            entity.ToTable("VaiTro");
        }
    }
}
