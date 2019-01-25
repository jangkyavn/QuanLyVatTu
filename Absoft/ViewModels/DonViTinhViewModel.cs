using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class DonViTinhViewModel
    {
        public DonViTinhViewModel()
        {
            Status = true;
        }
        public int? MaDVT { get; set; }
        public string TenDVT { get; set; }       
        public bool Status { set; get; }
    }
}
