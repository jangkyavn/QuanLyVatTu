using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class XuatVatTuViewModel
    {
        public XuatVatTuViewModel()
        {
            Status = true;
        }
        public int MaPhieuXuat { get; set; }
        public int MaKho { get; set; }
        public int MaNS { get; set; }
        public string NgayNhap { get; set; }
        public decimal TongSoTien { get; set; }
        public int TongSoLuong { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }
        public string TenKho { get; set; }
        public string TenNS { get; set; }
        public int ChietKhau { set; get; }
    }
}
