using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class ThanhLyChiTietViewModel
    {
        public ThanhLyChiTietViewModel()
        {
            this.Status = true;
        }
        public int MaPhieuThanhLy { get; set; }
        public int MaPhieuNhap { get; set; }
        public int MaVatTu { get; set; }
        public int? SoLuongThanhLy { get; set; }
        public string DienGiai { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }

        public string TenVT { get; set; }
        public string TenDVT { get; set; }
    }
}
