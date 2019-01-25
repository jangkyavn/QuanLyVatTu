using System.ComponentModel.DataAnnotations.Schema;

namespace Absoft.Data.Entities
{
    [Table("NhapChiTiet")]
    public class NhapChiTiet
    {
        public int MaPhieuNhap { get; set; }
        public int MaVatTu { get; set; }
        public int? MaNuoc { get; set; }
        public int? MaHang { get; set; }
        public string Model { get; set; }
        public string Seri { get; set; }
        public string SoKhung { get; set; }
        public string SoMay { get; set; }
        public string SoDangKy { get; set; }
        public string DotMua { get; set; }
        public string NamSX { get; set; }
        public string PhanCap { get; set; }
        public string NguonGoc { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }
    }
}
