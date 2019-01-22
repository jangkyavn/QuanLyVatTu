using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Absoft.Data.Entities
{
    [Table("XuatVatTu")]
    public class XuatVatTu
    {
        [Key]
        public int MaPhieuXuat { get; set; }
        public int MaKho { get; set; }
        public int MaNS { get; set; }
        public string NgayNhap { get; set; }
        public decimal TongSoTien { get; set; }
        public int TongSoLuong { get; set; }
        public string GhiChu { get; set; }
    }
}
