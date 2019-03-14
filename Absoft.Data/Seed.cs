using Absoft.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Absoft.Data
{
    public class Seed
    {
        private readonly UserManager<NguoiDung> _userManager;
        private readonly RoleManager<VaiTro> _roleManager;
        private readonly DataContext _dataContext;

        public Seed(UserManager<NguoiDung> userManager,
            RoleManager<VaiTro> roleManager,
            DataContext dataContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dataContext = dataContext;
        }

        public void SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                var roles = new List<VaiTro>
                {
                    new VaiTro() { Name= "Member"},
                    new VaiTro() { Name= "Admin"},
                    new VaiTro() { Name= "Moderator"}
                };

                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }

                var adminUser = new NguoiDung
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    FullName = "Administrator",
                    Gender = true,
                    DateOfBirth = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Status = true
                };

                var result = _userManager.CreateAsync(adminUser, "password").Result;
                if (result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("Admin").Result;
                    _userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" }).Wait();
                }
            }

            if (!_dataContext.ChucNangs.Any())
            {
                var models = new List<ChucNang>()
               {
                   new ChucNang() {
                       MaChucNang = "NGUOI_DUNG",
                       TenChucNang = "Người dùng",
                       ViTri = 1,
                       Status = true
                   },
                   new ChucNang() {
                       MaChucNang = "NGUON_CUNG_CAP",
                       TenChucNang = "Nguồn cung cấp",
                       ViTri = 3,
                       Status = true
                   },
                   new ChucNang() {
                       MaChucNang = "HANG_MUC_VAT_TU",
                       TenChucNang = "Hạng mục vật tư",
                       ViTri = 4,
                       Status = true
                   },
                   new ChucNang() {
                       MaChucNang = "LOAI_VAT_TU",
                       TenChucNang = "Loại vật tư",
                       ViTri = 5,
                       Status = true },
                   new ChucNang() {
                       MaChucNang = "VAT_TU",
                       TenChucNang = "Vật tư",
                       ViTri = 6,
                       Status = true },
                   new ChucNang() {
                       MaChucNang = "DON_VI_TINH",
                       TenChucNang = "Đơn vị tính",
                       ViTri = 7,
                       Status = true
                   },
                   new ChucNang() {
                       MaChucNang = "KHO_VAT_TU",
                       TenChucNang = "Kho vật tư",
                       ViTri = 8,
                       Status = true },
                   new ChucNang() {
                       MaChucNang = "HANG_SAN_XUAT",
                       TenChucNang = "Hãng sản xuất",
                       ViTri = 9,
                       Status = true
                   },
                   new ChucNang() {
                       MaChucNang = "NUOC_SAN_XUAT",
                       TenChucNang = "Nước sản xuất",
                       ViTri = 10,
                       Status = true
                   },
                   new ChucNang() {
                       MaChucNang = "NHAN_SU",
                       TenChucNang = "Nhân sự",
                       ViTri = 11,
                       Status = true
                   },
                   new ChucNang() {
                       MaChucNang = "NHAP_VAT_TU",
                       TenChucNang = "Nhập vật tư",
                       ViTri = 12,
                       Status = true
                   },
                   new ChucNang() {
                       MaChucNang = "XUAT_VAT_TU",
                       TenChucNang = "Xuất vật tư",
                       ViTri = 13,
                       Status = true
                   },
                   new ChucNang() {
                       MaChucNang = "VAT_TU_TON_KHO",
                       TenChucNang = "Vật tư tồn kho",
                       ViTri = 14,
                       Status = true
                   },
                   new ChucNang() {
                       MaChucNang = "THANH_LY_VAT_TU",
                       TenChucNang = "Thanh lý vật tư",
                       ViTri = 15,
                       Status = true
                   },
                    new ChucNang() {
                        MaChucNang = "PHIEU_KIEM_KE_KHO",
                        TenChucNang = "Phiếu kiểm kê kho",
                        ViTri = 16,
                        Status = true
                    },
                    new ChucNang() {
                        MaChucNang = "TIM_KIEM_VAT_TU",
                        TenChucNang = "Tìm kiếm vật tư",
                        ViTri = 17,
                        Status = true
                    },
                    new ChucNang() {
                        MaChucNang = "THONG_KE_NHAP",
                        TenChucNang = "Thống kê nhập",
                        ViTri = 18,
                        Status = true
                    },
                    new ChucNang() {
                        MaChucNang = "THONG_KE_XUAT",
                        TenChucNang = "Thống kê xuất",
                        ViTri = 19,
                        Status = true
                    },
                    new ChucNang() {
                        MaChucNang = "THONG_KE_THANH_LY",
                        TenChucNang = "Thống kê thanh lý",
                        ViTri = 20,
                        Status = true
                    },
                    new ChucNang() {
                        MaChucNang = "THONG_KE_NHAP_XUAT_TON",
                        TenChucNang = "Thống kê nhập xuất tồn",
                        ViTri = 21,
                        Status = true
                    },
               };

                _dataContext.ChucNangs.AddRange(models);
            }

            if (!_dataContext.HanhDongs.Any())
            {
                var models = new List<HanhDong>()
               {
                   new HanhDong() { MaHanhDong = "READ", TenHanhDong = "Xem", ViTri = 1, Status = true },
                   new HanhDong() { MaHanhDong = "CREATE", TenHanhDong = "Thêm", ViTri = 2, Status = true },
                   new HanhDong() { MaHanhDong = "UPDATE", TenHanhDong = "Sửa", ViTri = 3, Status = true },
                   new HanhDong() { MaHanhDong = "DELETE", TenHanhDong = "Xóa", ViTri = 4, Status = true },
               };
                _dataContext.HanhDongs.AddRange(models);
            }

            if (!_dataContext.ChucNangHanhDongs.Any())
            {
                var models = new List<ChucNangHanhDong>()
               {
                   new ChucNangHanhDong() { MaChucNang = "NGUOI_DUNG", MaHanhDong = "READ" },
                   new ChucNangHanhDong() { MaChucNang = "NGUOI_DUNG", MaHanhDong = "CREATE" },
                   new ChucNangHanhDong() { MaChucNang = "NGUOI_DUNG", MaHanhDong = "UPDATE" },
                   new ChucNangHanhDong() { MaChucNang = "NGUOI_DUNG", MaHanhDong = "DELETE" },

                   new ChucNangHanhDong() { MaChucNang = "NGUON_CUNG_CAP", MaHanhDong = "READ" },
                   new ChucNangHanhDong() { MaChucNang = "NGUON_CUNG_CAP", MaHanhDong = "CREATE" },
                   new ChucNangHanhDong() { MaChucNang = "NGUON_CUNG_CAP", MaHanhDong = "UPDATE" },
                   new ChucNangHanhDong() { MaChucNang = "NGUON_CUNG_CAP", MaHanhDong = "DELETE" },

                   new ChucNangHanhDong() { MaChucNang = "HANG_MUC_VAT_TU", MaHanhDong = "READ" },
                   new ChucNangHanhDong() { MaChucNang = "HANG_MUC_VAT_TU", MaHanhDong = "UPDATE" },

                   new ChucNangHanhDong() { MaChucNang = "LOAI_VAT_TU", MaHanhDong = "READ" },
                   new ChucNangHanhDong() { MaChucNang = "LOAI_VAT_TU", MaHanhDong = "CREATE" },
                   new ChucNangHanhDong() { MaChucNang = "LOAI_VAT_TU", MaHanhDong = "UPDATE" },
                   new ChucNangHanhDong() { MaChucNang = "LOAI_VAT_TU", MaHanhDong = "DELETE" },

                   new ChucNangHanhDong() { MaChucNang = "VAT_TU", MaHanhDong = "READ" },
                   new ChucNangHanhDong() { MaChucNang = "VAT_TU", MaHanhDong = "CREATE" },
                   new ChucNangHanhDong() { MaChucNang = "VAT_TU", MaHanhDong = "UPDATE" },
                   new ChucNangHanhDong() { MaChucNang = "VAT_TU", MaHanhDong = "DELETE" },

                   new ChucNangHanhDong() { MaChucNang = "DON_VI_TINH", MaHanhDong = "READ" },
                   new ChucNangHanhDong() { MaChucNang = "DON_VI_TINH", MaHanhDong = "CREATE" },
                   new ChucNangHanhDong() { MaChucNang = "DON_VI_TINH", MaHanhDong = "UPDATE" },
                   new ChucNangHanhDong() { MaChucNang = "DON_VI_TINH", MaHanhDong = "DELETE" },

                   new ChucNangHanhDong() { MaChucNang = "KHO_VAT_TU", MaHanhDong = "READ" },
                   new ChucNangHanhDong() { MaChucNang = "KHO_VAT_TU", MaHanhDong = "CREATE" },
                   new ChucNangHanhDong() { MaChucNang = "KHO_VAT_TU", MaHanhDong = "UPDATE" },
                   new ChucNangHanhDong() { MaChucNang = "KHO_VAT_TU", MaHanhDong = "DELETE" },

                   new ChucNangHanhDong() { MaChucNang = "HANG_SAN_XUAT", MaHanhDong = "READ" },
                   new ChucNangHanhDong() { MaChucNang = "HANG_SAN_XUAT", MaHanhDong = "CREATE" },
                   new ChucNangHanhDong() { MaChucNang = "HANG_SAN_XUAT", MaHanhDong = "UPDATE" },
                   new ChucNangHanhDong() { MaChucNang = "HANG_SAN_XUAT", MaHanhDong = "DELETE" },

                   new ChucNangHanhDong() { MaChucNang = "NUOC_SAN_XUAT", MaHanhDong = "READ" },
                   new ChucNangHanhDong() { MaChucNang = "NUOC_SAN_XUAT", MaHanhDong = "CREATE" },
                   new ChucNangHanhDong() { MaChucNang = "NUOC_SAN_XUAT", MaHanhDong = "UPDATE" },
                   new ChucNangHanhDong() { MaChucNang = "NUOC_SAN_XUAT", MaHanhDong = "DELETE" },

                   new ChucNangHanhDong() { MaChucNang = "NHAN_SU", MaHanhDong = "READ" },
                   new ChucNangHanhDong() { MaChucNang = "NHAN_SU", MaHanhDong = "CREATE" },
                   new ChucNangHanhDong() { MaChucNang = "NHAN_SU", MaHanhDong = "UPDATE" },
                   new ChucNangHanhDong() { MaChucNang = "NHAN_SU", MaHanhDong = "DELETE" },

                   new ChucNangHanhDong() { MaChucNang = "NHAP_VAT_TU", MaHanhDong = "READ" },
                   new ChucNangHanhDong() { MaChucNang = "NHAP_VAT_TU", MaHanhDong = "CREATE" },
                   new ChucNangHanhDong() { MaChucNang = "NHAP_VAT_TU", MaHanhDong = "UPDATE" },
                   new ChucNangHanhDong() { MaChucNang = "NHAP_VAT_TU", MaHanhDong = "DELETE" },

                   new ChucNangHanhDong() { MaChucNang = "XUAT_VAT_TU", MaHanhDong = "READ" },
                   new ChucNangHanhDong() { MaChucNang = "XUAT_VAT_TU", MaHanhDong = "CREATE" },
                   new ChucNangHanhDong() { MaChucNang = "XUAT_VAT_TU", MaHanhDong = "UPDATE" },
                   new ChucNangHanhDong() { MaChucNang = "XUAT_VAT_TU", MaHanhDong = "DELETE" },

                   new ChucNangHanhDong() { MaChucNang = "VAT_TU_TON_KHO", MaHanhDong = "READ" },

                   new ChucNangHanhDong() { MaChucNang = "THANH_LY_VAT_TU", MaHanhDong = "READ" },
                   new ChucNangHanhDong() { MaChucNang = "THANH_LY_VAT_TU", MaHanhDong = "CREATE" },
                   new ChucNangHanhDong() { MaChucNang = "THANH_LY_VAT_TU", MaHanhDong = "UPDATE" },
                   new ChucNangHanhDong() { MaChucNang = "THANH_LY_VAT_TU", MaHanhDong = "DELETE" },

                   new ChucNangHanhDong() { MaChucNang = "PHIEU_KIEM_KE_KHO", MaHanhDong = "READ" },
                   new ChucNangHanhDong() { MaChucNang = "PHIEU_KIEM_KE_KHO", MaHanhDong = "CREATE" },
                   new ChucNangHanhDong() { MaChucNang = "PHIEU_KIEM_KE_KHO", MaHanhDong = "UPDATE" },
                   new ChucNangHanhDong() { MaChucNang = "PHIEU_KIEM_KE_KHO", MaHanhDong = "DELETE" },

                   new ChucNangHanhDong() { MaChucNang = "TIM_KIEM_VAT_TU", MaHanhDong = "READ" },
               };

                _dataContext.ChucNangHanhDongs.AddRange(models);
            }

            _dataContext.SaveChanges();
        }
    }
}
