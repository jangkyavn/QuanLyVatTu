using Microsoft.AspNetCore.Identity;
using System;

namespace Absoft.Data.Entities
{
    public class VaiTroNguoiDung : IdentityUserRole<Guid>
    {
        public NguoiDung User { get; set; }
        public VaiTro Role { get; set; }
    }
}
