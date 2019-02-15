using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class NhapVatTuViewModel
    {
        public NhapVatTuViewModel()
        {
            Status = true;
        }
        public int? MaPhieuNhap { get; set; }
        public int MaHM { get; set; }
        public int MaKho { get; set; }
        public string TenHM { get; set; }
        public string TenKho { get; set; }
        public string NgayNhap { get; set; }
        public string NguoiNhap { get; set; }
        public decimal TongSoTien { get; set; }
        public int TongSoLuong { get; set; }
        public int ChietKhau { get; set; }
        public string GhiChu { get; set; }
        public decimal ThanhTien { get; set; }
        public bool Status { get; set; }           
    }
}
