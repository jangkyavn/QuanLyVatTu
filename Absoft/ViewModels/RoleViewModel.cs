using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Absoft.ViewModels
{
    public class RoleViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        [DefaultValue(false)]
        public bool Checked { get; set; }

        public List<PhanQuyenViewModel> PhanQuyens { get; set; }
    }
}
