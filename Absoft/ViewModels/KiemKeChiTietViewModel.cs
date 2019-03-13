using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class KiemKeChiTietViewModel
    {
        public KiemKeChiTietViewModel()
        {
            Status = true;
        }
        public int MaPhieuKiemKe { get; set; }
        public int MaPhieuNhap { get; set; }
        public int MaVatTu { get; set; }
        public int? SoLuongTheoDoi { get; set; }
        public int? SoLuongThucTon { get; set; }
        public int? SoLuongKiemKe { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }

        public string TenVatTu { get; set; }
    }
}
