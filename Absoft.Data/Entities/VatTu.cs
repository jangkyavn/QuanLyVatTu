using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Absoft.Data.Entities
{
    [Table("VatTu")]
    public class VatTu
    {
        [Key]
        public int MaVatTu { get; set; }
        public int MaLoaiVatTu { get; set; }
        public int? MaDVT { get; set; }
        public string TenVT { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }

        public virtual LoaiVatTu LoaiVatTu { get; set; }
        public virtual DonViTinh DonViTinh { get; set; }

        public ICollection<ThanhLyChiTiet> ThanhLyChiTiets { get; set; }
        public ICollection<KhoHang> KhoHangs { get; set; }
        public ICollection<XuatChiTiet> XuatChiTiets { get; set; }
        public ICollection<NhapChiTiet> NhapChiTiets { get; set; }
    }
}