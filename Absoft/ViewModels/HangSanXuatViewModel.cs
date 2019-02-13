namespace Absoft.ViewModels
{
    public class HangSanXuatViewModel
    {
        public HangSanXuatViewModel()
        {
            Status = true;
        }
        public int? MaHang { get; set; }
        public string TenHang { get; set; }
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
