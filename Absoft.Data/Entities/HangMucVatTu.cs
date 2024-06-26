﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Absoft.Data.Entities
{
    [Table("HangMucVatTu")]
    public class HangMucVatTu
    {
        [Key]
        public int MaHM { get; set; }
        public string TenHM { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }

        public ICollection<LoaiVatTu> LoaiVatTus { get; set; }
        public ICollection<NhapVatTu> NhapVatTus { get; set; }
    }
}