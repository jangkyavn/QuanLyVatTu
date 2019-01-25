using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

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
        public int SoLuongTon { get; set; }
        public string TenKho { get; set; }
        public string TenVatTu { get; set; }        
        public bool Status { get; set; }
    }
}
