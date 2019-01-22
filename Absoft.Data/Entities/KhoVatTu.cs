using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Absoft.Data
{
    [Table("KhoVatTu")]
    public class KhoVatTu
    {
        [Key]
        public int MaKho { get; set; }
        public string TenKho { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string GhiChu { get; set; }
    }
}