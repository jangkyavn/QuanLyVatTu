using Absoft.Helpers;
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
        private string tenDVT { get; set; } 
        public string TenDVT
        {
            get => tenDVT;
            set
            {
                tenDVT = value.ToTrim();
            }
        }
        public bool Status { set; get; }
        
    }
}
