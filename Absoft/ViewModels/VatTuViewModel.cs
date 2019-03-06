namespace Absoft.ViewModels
{
    public class VatTuViewModel
    {
        public VatTuViewModel()
        {
            Status = true;
        }
        public int? MaVatTu { get; set; }
        public int MaLoaiVatTu { get; set; }
        public int? MaDVT { get; set; }
        public string TenVT { get; set; }
        private string ghiChu;
        public string GhiChu
        {
            get => ghiChu;
            set
            {
                ghiChu = value ?? string.Empty;
            }
        }
        public string TenDVT { get; set; }
        public string TenLoaiVatTu { get; set; }
        public bool Status { set; get; }
        public string TenHM { get; set; }
        public int? TongTon { get; set; }
    }
}
