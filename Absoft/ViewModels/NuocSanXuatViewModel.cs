using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class NuocSanXuatViewModel
    {
        public NuocSanXuatViewModel()
        {
            Status = true;
        }

        public int? MaNuoc { get; set; }
        public string TenNuoc { get; set; }        
        public bool Status { set; get; }
    }
}
