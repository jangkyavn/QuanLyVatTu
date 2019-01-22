using Absoft.Data.Entities;
using Absoft.Data.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Absoft.Data.Configurations
{
    public class VaiTroNguoiDungConfiguration : DbEntityConfiguration<VaiTroNguoiDung>
    {
        public override void Configure(EntityTypeBuilder<VaiTroNguoiDung> entity)
        {
            entity.ToTable("VaiTroNguoiDung");

            entity.HasKey(x => new { x.UserId, x.RoleId });

            entity.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

            entity.HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }
    }
}
