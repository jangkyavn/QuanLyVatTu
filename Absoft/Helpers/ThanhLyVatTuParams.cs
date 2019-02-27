using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Helpers
{
    public class ThanhLyVatTuParams
    {
        public ThanhLyVatTuViewModel mthanhlyvattu { set; get; }
        public List<ThanhLyChiTietViewModel> listThanhlychitiet { get; set; }
    }
}
