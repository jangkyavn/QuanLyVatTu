using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class XuatChiTietViewModel
    {
        public XuatChiTietViewModel()
        {
            Status = true;
        }
        public int MaPhieuXuat { get; set; }
        public int MaPhieuNhap { get; set; }
        public int MaVatTu { get; set; }
        public int SoLuongXuat { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }
        public decimal DonGia { get; set; }
        public string TenVT { get; set; }
        public string TenDVT { get; set; }
    }
}
