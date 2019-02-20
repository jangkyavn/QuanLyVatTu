namespace Absoft.ViewModels
{
    public class NhapChiTietViewModel
    {
        public NhapChiTietViewModel()
        {
            Status = true;
        }
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
        public int? MaNguon { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string GhiChu { get; set; }        
        public bool Status { set; get; }
        public string BietDuoc { get; set; }
        public string SoLo { get; set; }
        public string HanDung { get; set; }
        public string TenVT { get; set; }
        public string TenNuoc { get; set; }
        public string TenHang { get; set; }
        public string TenNguon { get; set; }
        public string TenDVT { get; set; }
    }
}
