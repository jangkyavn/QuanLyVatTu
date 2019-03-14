using System.ComponentModel.DataAnnotations.Schema;

namespace Absoft.Data.Entities
{
    [Table("ChucNangHanhDong")]
    public class ChucNangHanhDong
    {
        public string MaChucNang { get; set; }
        public string MaHanhDong { get; set; }

        public ChucNang ChucNang { get; set; }
        public HanhDong HanhDong { get; set; }
    }
}
