using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class KhoVatTuViewModel
    {       
        public int? MaKho { get; set; }
        public string TenKho { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string GhiChu { get; set; }
        [DefaultValue(true)]
        public bool Status { set; get; }
    }
}
