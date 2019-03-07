namespace Absoft.ViewModels
{
    public class KhoHangViewModel
    {
        public KhoHangViewModel()
        {
            Status = true;
        }
        public int MaKho { get; set; }
        public int MaPhieuNhap { get; set; }
        public int MaVatTu { get; set; }
        public int? SoLuongTon { get; set; }
        public string TenKho { get; set; }
        public string TenVatTu { get; set; }        
        public bool Status { get; set; }
        public string NgayNhap { get; set; }
    }
}
