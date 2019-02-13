namespace Absoft.ViewModels
{
    public class NguonCungCapViewModel
    {
        public NguonCungCapViewModel()
        {
            Status = true;
        }
        public int? MaNguon { get; set; }
        public string TenNguon { get; set; }
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
