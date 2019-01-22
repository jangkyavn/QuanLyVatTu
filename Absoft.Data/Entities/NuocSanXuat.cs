using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Absoft.Data
{
    [Table("NuocSanXuat")]
    public class NuocSanXuat
    {
        [Key]
        public int MaNuoc { get; set; }
        public string TenNuoc { get; set; }
    }
}