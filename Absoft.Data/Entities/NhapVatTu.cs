﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Absoft.Data.Entities
{
    [Table("NhapVatTu")]
    public class NhapVatTu
    {
        [Key]
        public int MaPhieuNhap { get; set; }
        public string SoPhieuNhap { get; set; }
        public int MaHM { get; set; }
        public int MaKho { get; set; }
        public string NgayNhap { get; set; }
        public string NguoiNhap { get; set; }
        public decimal? TongSoTien { get; set; }
        public int? TongSoLuong { get; set; }
        public decimal? ChietKhau { get; set; }
        public string GhiChu { get; set; }
        public bool Status { get; set; }

        public KhoVatTu KhoVatTu { get; set; }
        public HangMucVatTu HangMucVatTu { get; set; }
    }
}
