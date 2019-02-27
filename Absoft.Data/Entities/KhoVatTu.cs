using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Absoft.Data.Entities
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
        public bool Status { set; get; }

        public ICollection<NhapVatTu> NhapVatTus { get; set; }
        public ICollection<XuatVatTu> XuatVatTus { get; set; }
        public ICollection<ThanhLyVatTu> ThanhLyVatTus { get; set; }
        public ICollection<KhoHang> KhoHangs { get; set; }
    }
}