using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class VatTuViewModel
    {
        public int? MaVatTu { get; set; }
        public int MaLoaiVatTu { get; set; }
        public int MaDVT { get; set; }
        public string TenVT { get; set; }
        public string GhiChu { get; set; }
        public string TenDVT { get; set; }
        public string TenLoaiVatTu { get; set; }
    }
}
