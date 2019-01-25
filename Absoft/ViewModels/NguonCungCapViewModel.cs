using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class NguonCungCapViewModel
    {
        public NguonCungCapViewModel()
        {
            Status = true;
        }
        public int? MaNguon { get; set; }
        public string TenNguon { get; set; }
        public string GhiChu { get; set; }        
        public bool Status { set; get; }
    }
}
