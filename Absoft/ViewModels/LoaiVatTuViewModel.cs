using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

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
        public string GhiChu { get; set; }
        public string TenHM { get; set; }        
        public bool Status { set; get; }
    }
}
