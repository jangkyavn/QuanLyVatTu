namespace Absoft.ViewModels
{
    public class HangMucVatTuViewModel
    {
        public HangMucVatTuViewModel()
        {
            Status = true;
        }

        public int? MaHM { get; set; }
        public string TenHM { get; set; }
        private string ghiChu;
        public string GhiChu
        {
            get => ghiChu;
            set
            {
                ghiChu = value ?? string.Empty;
            }
        }
        public bool Status { set; get; }
    }
}
