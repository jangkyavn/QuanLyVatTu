using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Absoft.Data
{
    [Table("LoaiVatTu")]
    public class LoaiVatTu
    {
        [Key]
        public int MaLoaiVatTu { get; set; }
        public int MaHM { get; set; }        
        public string TenLoai { get; set; }
        public string GhiChu { get; set; }
    }
}