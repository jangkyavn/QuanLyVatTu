using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Absoft.Data.Entities
{
    [Table("HangSanXuat")]
    public class HangSanXuat
    {
        [Key]
        public int MaHang { get; set; }
        public string TenHang { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }
    }
}