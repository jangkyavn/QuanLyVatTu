namespace Absoft.ViewModels
{
    public class NhapVatTuViewModel
    {
        public NhapVatTuViewModel()
        {
            Status = true;
            ChietKhau = 0;
        }
        public int? MaPhieuNhap { get; set; }
        public string SoPhieuNhap { get; set; }
        public int MaHM { get; set; }
        public int MaKho { get; set; }
        public string TenHM { get; set; }
        public string TenKho { get; set; }
        public string NgayNhap { get; set; }
        public string NguoiNhap { get; set; }
        public decimal? TongSoTien { get; set; }
        public int? TongSoLuong { get; set; }
        public float? ChietKhau { get; set; }
        public string GhiChu { get; set; }
        public decimal? ThanhTien { get; set; }
        public bool Status { get; set; }
    }
}
