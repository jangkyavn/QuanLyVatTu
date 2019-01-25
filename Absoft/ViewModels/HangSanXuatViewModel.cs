using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class HangSanXuatViewModel
    {
        public HangSanXuatViewModel()
        {
            Status = true;
        }
        public int? MaHang { get; set; }
        public string TenHang { get; set; }
        public string GhiChu { get; set; }        
        public bool Status { set; get; }
    }
}
