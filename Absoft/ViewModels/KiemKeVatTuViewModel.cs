using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class KiemKeVatTuViewModel
    {
        public KiemKeVatTuViewModel()
        {
            Status = true;
        }
        public int MaPhieuKiemKe { get; set; }
        public string SoPhieuKiemKe { get; set; }
        public int MaKho { get; set; }
        public int MaNS { get; set; }
        public string NgayKiemKe { get; set; }
        public int? TongTheoDoi { get; set; }
        public int? TongThucTon { get; set; }
        public int? TongKiemKe { get; set; }
        
        public bool Status { set; get; }

        public string TenKho { get; set; }
        public string TenNS { get; set; }
    }
}
