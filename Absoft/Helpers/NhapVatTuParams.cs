using Absoft.ViewModels;
using System.Collections.Generic;

namespace Absoft.Helpers
{
    public class NhapVatTuParams
    {
        public NhapVatTuViewModel mnhapvattu { set; get; }
        public List<NhapChiTietViewModel> listnhapchitiet { get; set; }
    }
}
