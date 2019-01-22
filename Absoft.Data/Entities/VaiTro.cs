using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Absoft.Data.Entities
{
    public class VaiTro : IdentityRole<Guid>
    {
        public virtual ICollection<VaiTroNguoiDung> UserRoles { get; set; }
    }
}
