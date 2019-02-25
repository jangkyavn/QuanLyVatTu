using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Helpers
{
    public class ThongKeVatTuParam
    {     
        public int MaVatTu { get; set; }
        public int MaLoaiVatTu { get; set; }
        public int MaHM { get; set; }
        public int? MaDVT { get; set; }
       
        public bool Status { set; get; }
        public string GhiChu { set; get; }

        public string TenVT { get; set; }
        public string TenDVT { get; set; }
        public string TenLoaiVatTu { get; set; }       
        public string TenHM { get; set; }

        public int? MaPN { get; set; }
        public int? MaPX { get; set; }
        public int? MaKho { get; set; }

        public string NgayNhap { get; set; }
        public string NgayXuat { get; set; }
        public int? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public decimal?TongTien { get; set; }
        public decimal? ChietKhau { get; set; }
        public decimal? ThanhTien { get; set; }
      

    }
}
