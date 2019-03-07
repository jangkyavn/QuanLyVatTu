using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Helpers
{
    public class PagedListParam
    {
        public PagedList<ThongKeVatTuParam> pl { get; set; }
        public PagedList<KhoHangViewModel> plkho { get; set; }       
        public int? tongluong { get; set; }
        public decimal? tongtien { get; set; }
    }
}
