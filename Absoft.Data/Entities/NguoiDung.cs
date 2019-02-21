using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Absoft.Data.Entities
{
    public class NguoiDung : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public bool? Gender { get; set; }
        public string Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<VaiTroNguoiDung> UserRoles { get; set; }
    }
}
