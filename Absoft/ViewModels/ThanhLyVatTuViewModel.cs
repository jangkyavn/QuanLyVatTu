using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class ThanhLyVatTuViewModel
    {
        public ThanhLyVatTuViewModel()
        {
            this.Status = true;
        }
        public int MaPhieuThanhLy { get; set; }
        public string SoPhieuThanhLy { get; set; }
        public int MaKho { get; set; }
        public int MaNS { get; set; }
        public string NgayThanhLy { get; set; }
        public int? TongSoLuong { get; set; }
        public bool Status { set; get; }

        public string TenKho { get; set; }
        public string TenNS { get; set; }
    }
}
