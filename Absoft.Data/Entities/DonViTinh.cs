using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Absoft.Data.Entities
{
    [Table("DonViTinh")]
    public class DonViTinh
    {
        [Key]
        public int MaDVT { get; set; }
        public string TenDVT { get; set; }
        public bool Status { set; get; }

        public ICollection<VatTu> VatTus { get; set; }
    }
}