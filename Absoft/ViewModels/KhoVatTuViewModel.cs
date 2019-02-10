using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

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
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
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
