using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Helpers
{
    public class KiemKeVatTuParams
    {
        public KiemKeVatTuViewModel mKiemKeVatTu { set; get; }
        public List<KiemKeChiTietViewModel> listKiemKeChiTiet { get; set; }
    }
}

