﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.ViewModels
{
    public class HangMucVatTuViewModel
    {       
        public int? MaHM { get; set; }
        public string TenHM { get; set; }
        public string GhiChu { get; set; }
        [DefaultValue(true)]
        public bool Status { set; get; }
    }
}
