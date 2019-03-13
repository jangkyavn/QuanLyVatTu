using System;

namespace Absoft.Helpers
{
    public class PermissionParams
    {
        public Guid RoleId { get; set; }
        public string FunctionId { get; set; }
        public string ActionId { get; set; }
    }
}
