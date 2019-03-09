using Absoft.ViewModels;
using System;
using System.Collections.Generic;

namespace Absoft.Helpers
{
    public class PermissionParams
    {
        public List<PhanQuyenViewModel> Permissions { get; set; }
        public Guid RoleId { get; set; }
    }
}
