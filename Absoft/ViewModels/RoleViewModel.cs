using System;
using System.ComponentModel;

namespace Absoft.ViewModels
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [DefaultValue(false)]
        public bool Checked { get; set; }
    }
}
