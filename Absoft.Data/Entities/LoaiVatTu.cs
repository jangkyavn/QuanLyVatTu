using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Absoft.Data.Entities
{
    [Table("LoaiVatTu")]
    public class LoaiVatTu
    {
        [Key]
        public int MaLoaiVatTu { get; set; }
        public int MaHM { get; set; }        
        public string TenLoai { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }

        public HangMucVatTu HangMucVatTu { get; set; }

        public ICollection<VatTu> VatTus { get; set; }
    }
}