using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Absoft.Data.Entities
{
    [Table("HanhDong")]
    public class HanhDong
    {
        [Key]
        public string MaHanhDong { get; set; }
        public string TenHanhDong { get; set; }
        public bool Status { get; set; }

        public ICollection<PhanQuyen> PhanQuyens { get; set; }
    }
}
