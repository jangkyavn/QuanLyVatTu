namespace Absoft.Helpers
{
    public class ThongKeVatTuParam
    {     
        public bool Status { set; get; }
        public string GhiChu { set; get; }
        public string DienGiai { set; get; }

        public int? MaPN { get; set; }
        public int? MaPX { get; set; }
        public int? MaPTL { get; set; }
        public int? MaKho { get; set; }
        public decimal? ChietKhau { get; set; }

        public string NgayNhap { get; set; }
        public string NgayXuat { get; set; }
        public string NgayThanhLy { get; set; }

        public int? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? ThanhTien { get; set; }
    }
}
