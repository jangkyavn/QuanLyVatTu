using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Absoft.Data.Entities
{
    [Table("KiemKeVatTu")]
    public class KiemKeVatTu
    {
        [Key]
        public int MaPhieuKiemKe { get; set; }
        public string SoPhieuKiemKe { get; set; }
        public int MaKho { get; set; }
        public int MaNS { get; set; }
        public string NgayKiemK { get; set; }
        public int? TongTheoDoi { get; set; }
        public int? TongThucTon { get; set; }
        public bool Status { set; get; }

        public KhoVatTu KhoVatTu { get; set; }
        public NhanSu NhanSu { get; set; }

        public ICollection<KiemKeChiTiet> KiemKeChiTiets { get; set; }
    }
}
