using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class NguoiDungConfiguration : DbEntityConfiguration<NguoiDung>
    {
        public override void Configure(EntityTypeBuilder<NguoiDung> entity)
        {
            entity.ToTable("NguoiDung");

            entity.Property(p => p.UserName).IsRequired().HasMaxLength(20).IsUnicode(false);
            entity.Property(p => p.FullName).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Email).IsRequired().HasMaxLength(50).IsUnicode(false);
            entity.Property(p => p.PhoneNumber).HasMaxLength(20).IsUnicode(false);
            entity.Property(p => p.Avatar).HasMaxLength(50).IsUnicode(false);
            entity.Property(p => p.Address).HasMaxLength(100);
        }
    }
}
