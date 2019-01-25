using System.ComponentModel.DataAnnotations.Schema;

namespace Absoft.Data.Entities
{
    [Table("XuatChiTiet")]
    public class XuatChiTiet
    {
        public int MaPhieuXuat { get; set; }
        public int MaPhieuNhap { get; set; }
        public int MaVatTu { get; set; }
        public int SoLuongXuat { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }
        public decimal DonGia { get; set; }
    }
}
