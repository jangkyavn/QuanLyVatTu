using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

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
        public string GhiChu { get; set; }        
        public bool Status { set; get; }
    }
}
