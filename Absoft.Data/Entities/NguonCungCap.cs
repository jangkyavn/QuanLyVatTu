using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Absoft.Data.Entities
{
    [Table("NguonCungCap")]
    public class NguonCungCap
    {
        [Key]
        public int MaNguon { get; set; }
        public string TenNguon { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }
    }
}