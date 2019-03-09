using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Absoft.Data.Entities
{
    [Table("PhanQuyen")]
    public class PhanQuyen
    {
        public Guid MaVaiTro { get; set; }
        public string MaChucNang { get; set; }
        public string MaHanhDong { get; set; }

        public VaiTro VaiTro { get; set; }
        public ChucNang ChucNang { get; set; }
        public HanhDong HanhDong { get; set; }
    }
}
