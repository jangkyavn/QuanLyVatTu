namespace Absoft.ViewModels
{
    public class KhoVatTuViewModel
    {
        public KhoVatTuViewModel()
        {
            Status = true;
        }

        public int? MaKho { get; set; }
        public string TenKho { get; set; }
        private string diaChi { get; set; }
        public string DiaChi
        {
            get => diaChi;
            set
            {
                diaChi = value ?? string.Empty;
            }
        }
        private string dienThoai { get; set; }
        public string DienThoai
        {
            get => dienThoai;
            set
            {
                dienThoai = value ?? string.Empty;
            }
        }
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
