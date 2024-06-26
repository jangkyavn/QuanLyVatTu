﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Absoft.Data.Entities
{
    [Table("KhoHang")]
    public class KhoHang
    {
        public int MaKho { get; set; }
        public int MaPhieuNhap { get; set; }
        public int MaVatTu { get; set; }
        public int? SoLuongTon { get; set; }
        public bool Status { set; get; }

        public KhoVatTu KhoVatTu { get; set; }
        public NhapVatTu NhapVatTu { get; set; }
        public VatTu VaTu { get; set; }
    }
}
