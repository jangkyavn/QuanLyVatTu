namespace Absoft.ViewModels
{
    public class XuatVatTuViewModel
    {
        public XuatVatTuViewModel()
        {
            Status = true;
            ChietKhau = 0;
        }
        public int MaPhieuXuat { get; set; }
        public string SoPhieuXuat { get; set; }
        public int MaKho { get; set; }
        public int MaNS { get; set; }
        public string NgayNhap { get; set; }
        public decimal? TongSoTien { get; set; }
        public int? TongSoLuong { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }
        public string TenKho { get; set; }
        public string TenNS { get; set; }
        public decimal? ChietKhau { set; get; }
        public decimal? ThanhTien { get; set; }
    }
}
