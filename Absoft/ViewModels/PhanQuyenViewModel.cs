using System;

namespace Absoft.ViewModels
{
    public class PhanQuyenViewModel
    {
        public Guid MaVaiTro { get; set; }
        public string MaChucNang { get; set; }
        public string MaHanhDong { get; set; }

        public RoleViewModel VaiTro { get; set; }
        public ChucNangViewModel ChucNang { get; set; }
        public HanhDongViewModel HanhDong { get; set; }
    }
}
