using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class NuocSanXuatViewModel
    {       
        public int? MaNuoc { get; set; }
        public string TenNuoc { get; set; }
        [DefaultValue(true)]
        public bool Status { set; get; }
    }
}
