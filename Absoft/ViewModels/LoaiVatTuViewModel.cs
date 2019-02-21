namespace Absoft.ViewModels
{
    public class LoaiVatTuViewModel
    {
        public LoaiVatTuViewModel()
        {
            Status = true;
        }

        public int? MaLoaiVatTu { get; set; }
        public int? MaHM { get; set; }
        public string TenLoai { get; set; }
        private string ghiChu;
        public string GhiChu
        {
            get => ghiChu;
            set
            {
                ghiChu = value ?? string.Empty;
            }
        }
        public string TenHM { get; set; }
        public bool Status { set; get; }
    }
}
