using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Absoft.Data.Entities
{
    [Table("ThanhLyChiTiet")]
    public class ThanhLyChiTiet
    {
        public int MaPhieuThanhLy { get; set; }
        public int MaPhieuNhap { get; set; }
        public int MaVatTu { get; set; }
        public int? SoLuongThanhLy { get; set; }
        public string DienGiai { get; set; }
        public string GhiChu { get; set; }        
        public bool Status { set; get; }

        public ThanhLyVatTu ThanhLyVatTu { get; set; }
        public NhapVatTu NhapVatTu { get; set; }
        public VatTu VatTu { get; set; }
    }
}
