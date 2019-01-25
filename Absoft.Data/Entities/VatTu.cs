using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Absoft.Data.Entities
{
    [Table("VatTu")]
    public class VatTu
    {
        [Key]
        public int MaVatTu { get; set; }
        public int MaLoaiVatTu { get; set; }
        public int MaDVT { get; set; }
        public string TenVT { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }
    }
}