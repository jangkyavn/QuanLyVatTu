using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Absoft.Data.Entities
{
    [Table("ThanhLyVatTu")]
    public class ThanhLyVatTu
    {
        [Key]
        public int MaPhieuThanhLy { get; set; }
        public string SoPhieuThanhLy { get; set; }
        public int MaKho { get; set; }
        public int MaNS { get; set; }
        public string NgayThanhLy { get; set; }      
        public int? TongSoLuong { get; set; }   
        public bool Status { set; get; }        

        public KhoVatTu KhoVatTu { get; set; }
        public NhanSu NhanSu { get; set; }

        public ICollection<ThanhLyChiTiet> ThanhLyChiTiets { get; set; }
    }
}
