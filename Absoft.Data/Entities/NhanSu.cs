using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Absoft.Data.Entities
{
    [Table("NhanSu")]
    public class NhanSu
    {
        [Key]
        public int MaNS { get; set; }
        public string HoTen { get; set; }
        public string NgaySinh { get; set; }
        public string QueQuan { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string CapBacSHSQ { get; set; }
        public string ChucVu { get; set; }
        public string NgayNhapNguTuyenDung { get; set; }
        public string XuatThan { get; set; }
        public string BanThan { get; set; }
        public string QuaTrinhHocTap { get; set; }
        public string ThanNhan { get; set; }
        public string GhiChu { get; set; }
        public bool Status { set; get; }
    }
}