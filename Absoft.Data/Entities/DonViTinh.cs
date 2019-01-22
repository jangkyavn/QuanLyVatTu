using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Absoft.Data
{
    [Table("DonViTinh")]
    public class DonViTinh
    {
        [Key]
        public int MaDVT { get; set; }
        public string TenDVT { get; set; }
    }
}