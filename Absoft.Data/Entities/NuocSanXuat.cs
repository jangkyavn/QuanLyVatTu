using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Absoft.Data.Entities
{
    [Table("NuocSanXuat")]
    public class NuocSanXuat
    {
        [Key]
        public int MaNuoc { get; set; }
        public string TenNuoc { get; set; }
        public bool Status { set; get; }

        public ICollection<NhapChiTiet> NhapChiTiets { get; set; }
    }
}