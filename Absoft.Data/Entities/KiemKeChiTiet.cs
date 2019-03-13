using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Absoft.Data.Entities
{
    [Table("KiemKeChiTiet")]
    public class KiemKeChiTiet
    {
        public int MaPhieuKiemKe { get; set; }
        public int MaPhieuNhap { get; set; }
        public int MaVatTu { get; set; }
        public int? SoLuongTheoDoi { get; set; }
        public int? SoLuongThucTon { get; set; }
        public int? SoLuongKiemKe { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }

        public KiemKeVatTu KiemKeVatTu { get; set; }
        public NhapVatTu NhapVatTu { get; set; }
        public VatTu VatTu { get; set; }
    }
}
