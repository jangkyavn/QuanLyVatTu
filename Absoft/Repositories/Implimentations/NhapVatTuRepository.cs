using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class NhapVatTuRepository : INhapVatTuRepository
    {
        DataContext db;
        IMapper mp;
        INhapChiTietRepository inhapchitiet;
        IKhoHangRepository ikhohang;
        public NhapVatTuRepository(DataContext data, IMapper mapper, INhapChiTietRepository INhapChiTietRepository, IKhoHangRepository IKhoHangRepository)
        {
            db = data;
            mp = mapper;
            inhapchitiet = INhapChiTietRepository;
            ikhohang = IKhoHangRepository;
        }

        public async Task<bool> DeleteAsync(int maPN)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // tim ban ghi theo maphieu nhap
                    var nvt = await db.NhapVatTus.FindAsync(maPN);
                    // sua cac truong tru tong tien, tong sl                  
                    // sua trong chi tiet
                    var listnhapchitiet = db.NhapChiTiets.Where(x => x.MaPhieuNhap == maPN).ToList();
                    foreach (var item in listnhapchitiet)
                    {
                        var check = await inhapchitiet.DeleteNhapChiTietAsync(maPN, item.MaVatTu, nvt.MaKho);
                        if (check == false)
                        {
                            return false; // loi vi chi tiet da xuat, tra ve chi tiet
                        }
                    }
                    db.NhapVatTus.Remove(nvt);
                    transaction.Commit();
                    await db.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    // TODO: Handle failure                         
                }
            }
            return false;
        }
        public async Task<List<NhapVatTuViewModel>> GetAllAsync()
        {
            var model = from nvt in db.NhapVatTus
                        join hm in db.HangMucVatTus on nvt.MaHM equals hm.MaHM
                        join kvt in db.KhoVatTus on nvt.MaKho equals kvt.MaKho
                        select new NhapVatTuViewModel
                        {
                            MaPhieuNhap = nvt.MaPhieuNhap,
                            MaHM = nvt.MaHM,
                            MaKho = nvt.MaKho,
                            TenHM = hm.TenHM,
                            TenKho = kvt.TenKho,
                            NgayNhap = nvt.NgayNhap,
                            NguoiNhap = nvt.NguoiNhap,
                            TongSoTien = nvt.TongSoTien,
                            TongSoLuong = nvt.TongSoLuong,
                            ChietKhau = nvt.ChietKhau,
                            GhiChu = nvt.GhiChu,
                            Status = nvt.Status
                        };
            return await model.ToListAsync();
        }

        public async Task<PagedList<NhapVatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from nvt in db.NhapVatTus
                        join hm in db.HangMucVatTus on nvt.MaHM equals hm.MaHM
                        join kvt in db.KhoVatTus on nvt.MaKho equals kvt.MaKho
                        orderby nvt.NgayNhap descending
                        select new NhapVatTuViewModel
                        {
                            MaPhieuNhap = nvt.MaPhieuNhap,
                            MaHM = nvt.MaHM,
                            MaKho = nvt.MaKho,
                            TenHM = hm.TenHM,
                            TenKho = kvt.TenKho,
                            NgayNhap = nvt.NgayNhap,
                            NguoiNhap = nvt.NguoiNhap,
                            TongSoTien = nvt.TongSoTien,
                            TongSoLuong = nvt.TongSoLuong,
                            ChietKhau = nvt.ChietKhau,
                            GhiChu = nvt.GhiChu,
                            ThanhTien = (decimal)((double)nvt.TongSoTien * (1 - ((double)nvt.ChietKhau / 100))),
                            Status = nvt.Status
                        };


            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                if (DateTime.TryParseExact(keyword, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    query = query.Where(x => DateTime.Parse(x.NgayNhap).Day == date.Day && DateTime.Parse(x.NgayNhap).Month == DateTime.Parse(x.NgayNhap).Month && DateTime.Parse(x.NgayNhap).Year == date.Year);
                }
                else
                {
                    query = query.Where(x => x.TenKho.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenKho.ToUpper().Contains(keyword) ||
                                        x.TenHM.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenHM.ToUpper().Contains(keyword) ||
                                        x.TongSoTien.ToString().Equals(keyword) ||
                                        x.TongSoLuong.ToString().Equals(keyword) ||
                                        x.ChietKhau.ToString().Equals(keyword) ||
                                        x.ThanhTien.ToString().Equals(keyword));
                }
            }

            if (!string.IsNullOrEmpty(pagingParams.toDate) && !string.IsNullOrEmpty(pagingParams.fromDate))
            {
                var fromDate = pagingParams.fromDate;
                var toDate = pagingParams.toDate;
                query = query.Where(x => DateTime.Parse(x.NgayNhap) >= DateTime.Parse(fromDate) && DateTime.Parse(x.NgayNhap) <= DateTime.Parse(toDate));
            }


            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "tenKho":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenKho);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenKho);
                        }
                        break;
                    case "tenHM":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenHM);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenHM);
                        }
                        break;
                    case "ngayNhap":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.NgayNhap);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.NgayNhap);
                        }
                        break;
                    case "tongSoTien":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TongSoTien);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TongSoTien);
                        }
                        break;
                    case "tongSoLuong":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TongSoLuong);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TongSoLuong);
                        }
                        break;
                    case "chietKhau":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.ChietKhau);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.ChietKhau);
                        }
                        break;
                    case "thanhTien":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.ThanhTien);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.ThanhTien);
                        }
                        break;
                    default:
                        break;
                }
            }

            return await PagedList<NhapVatTuViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<NhapVatTuParams> GetDetailAsync(int maPN)
        {
            var queryNhapVT = await (from nvt in db.NhapVatTus
                                     join kvt in db.KhoVatTus on nvt.MaKho equals kvt.MaKho
                                     join hm in db.HangMucVatTus on nvt.MaHM equals hm.MaHM
                                     where nvt.MaPhieuNhap == maPN
                                     select new NhapVatTuViewModel
                                     {
                                         MaKho = nvt.MaKho,
                                         MaHM = nvt.MaHM,
                                         MaPhieuNhap = nvt.MaPhieuNhap,
                                         NgayNhap = nvt.NgayNhap,
                                         ChietKhau = nvt.ChietKhau,
                                         GhiChu = nvt.GhiChu,
                                         NguoiNhap = nvt.NguoiNhap,
                                         Status = nvt.Status,
                                         TenHM = hm.TenHM,
                                         TenKho = kvt.TenKho,
                                         TongSoLuong = nvt.TongSoLuong,
                                         TongSoTien = nvt.TongSoTien,
                                         ThanhTien = (decimal)((double)nvt.TongSoTien * (1 - ((double)nvt.ChietKhau / 100)))
                                     }).FirstOrDefaultAsync();

            var chiTietVM = await (from ct in db.NhapChiTiets
                                   join vt in db.VatTus on ct.MaVatTu equals vt.MaVatTu
                                   join dvt in db.DonViTinhs on vt.MaDVT equals dvt.MaDVT into tmpDonViTinhs
                                   join nsx in db.NuocSanXuats on ct.MaNuoc equals nsx.MaNuoc into tmpNuocSanXuats
                                   join hsx in db.HangSanXuats on ct.MaHang equals hsx.MaHang into tmpHangSanXuats
                                   join ncc in db.NguonCungCaps on ct.MaNguon equals ncc.MaNguon into tmpNguonCungCaps
                                   from dvt in tmpDonViTinhs.DefaultIfEmpty()
                                   from nsx in tmpNuocSanXuats.DefaultIfEmpty()
                                   from hsx in tmpHangSanXuats.DefaultIfEmpty()
                                   from ncc in tmpNguonCungCaps.DefaultIfEmpty()
                                   where ct.MaPhieuNhap == maPN
                                   select new NhapChiTietViewModel
                                   {
                                       MaPhieuNhap = ct.MaPhieuNhap,
                                       MaVatTu = ct.MaVatTu,
                                       TenVT = vt.TenVT,
                                       DonGia = ct.DonGia,
                                       SoLuong = ct.SoLuong,
                                       MaHang = ct.MaHang,
                                       TenHang = hsx.TenHang,
                                       MaNuoc = ct.MaNuoc,
                                       TenNuoc = nsx.TenNuoc,
                                       Model = ct.Model,
                                       Seri = ct.Seri,
                                       SoKhung = ct.SoKhung,
                                       SoMay = ct.SoMay,
                                       SoDangKy = ct.SoDangKy,
                                       DotMua = ct.DotMua,
                                       NamSX = ct.NamSX,
                                       PhanCap = ct.PhanCap,
                                       MaNguon = ct.MaNguon,
                                       TenNguon = ncc.TenNguon,
                                       GhiChu = ct.GhiChu,
                                       TenDVT = dvt.TenDVT,
                                       BietDuoc = ct.BietDuoc,
                                       SoLo = ct.SoLo,
                                       HanDung = ct.HanDung,
                                       Status = ct.Status
                                   }).ToListAsync();

            return new NhapVatTuParams()
            {
                mnhapvattu = queryNhapVT,
                listnhapchitiet = chiTietVM
            };
        }
        public async Task<bool> InsertAsync(NhapVatTuViewModel mnhapvattu, List<NhapChiTietViewModel> listnhapchitiet)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    mnhapvattu.TongSoLuong = 0;
                    mnhapvattu.TongSoTien = 0;
                    var pn = mp.Map<NhapVatTu>(mnhapvattu);
                    await db.NhapVatTus.AddAsync(pn);
                    var rs = await db.SaveChangesAsync();
                    if (rs > 0)
                    {
                        // gọi hàm nhập vào phiếu nhập chi tiết
                        foreach (var item in listnhapchitiet)
                        {
                            var result = await inhapchitiet.InsertAsync(item, pn.MaPhieuNhap);
                            if (result == true)
                            {
                                mnhapvattu.TongSoTien += item.DonGia * item.SoLuong;
                                mnhapvattu.TongSoLuong += item.SoLuong;
                                // insert vào kho hàng theo chi tiết
                                var khohang = new KhoHangViewModel()
                                {
                                    MaKho = pn.MaKho,
                                    MaPhieuNhap = pn.MaPhieuNhap,
                                    MaVatTu = item.MaVatTu,
                                    SoLuongTon = item.SoLuong
                                };
                                // gọi hàm nhập vào kho
                                await ikhohang.InsertAsync(khohang);
                            }
                            else return false;
                        }
                        // cập nhật tổng sl, tổng tiền vào phiếu nhập 
                        pn.TongSoLuong = mnhapvattu.TongSoLuong;
                        pn.TongSoTien = mnhapvattu.TongSoTien;
                        db.NhapVatTus.Update(pn);
                        transaction.Commit();
                        return await db.SaveChangesAsync() > 0;
                    }
                    else return false;
                }
                catch (Exception)
                {
                    // TODO: Handle failure                    
                }
            }
            return false;
        }

        public async Task<int> InsertNhapVatTuAsync(NhapVatTuViewModel mnhapvattu)
        {
            mnhapvattu.TongSoLuong = 0;
            mnhapvattu.TongSoTien = 0;
            var pn = mp.Map<NhapVatTu>(mnhapvattu);
            await db.NhapVatTus.AddAsync(pn);
            var rs = await db.SaveChangesAsync();
            return pn.MaPhieuNhap;
        }
        public async Task<int> UpdateAsync(NhapVatTuViewModel mnhapvattu, List<NhapChiTietViewModel> listnhapchitiet)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var model = await db.NhapVatTus.FindAsync(mnhapvattu.MaPhieuNhap);
                    mnhapvattu.TongSoLuong = model.TongSoLuong;
                    mnhapvattu.TongSoTien = model.TongSoTien;
                    // tim ban ghi theo maphieu nhap
                    var nvt = mp.Map<NhapVatTu>(mnhapvattu);
                    var nvtcu = mp.Map<NhapVatTu>(model);
                    // sua cac truong tru tong tien, tong sl
                    //db.NhapVatTus.Update(nvt);
                    db.Entry(nvtcu).CurrentValues.SetValues(nvt);


                    // sua trong chi tiet
                    foreach (var item in listnhapchitiet)
                    {
                        // cộng vào tổng ct mới
                        mnhapvattu.TongSoTien += item.DonGia * item.SoLuong;
                        mnhapvattu.TongSoLuong += item.SoLuong;
                        var ctu = await db.NhapChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == item.MaPhieuNhap && x.MaVatTu == item.MaVatTu);
                        // trừ trong tổng ct cũ
                        mnhapvattu.TongSoLuong -= ctu.SoLuong;
                        mnhapvattu.TongSoTien -= ctu.SoLuong * ctu.DonGia;
                        //
                        int sltonmoi = await inhapchitiet.UpdateNhapChiTietAsync(item, mnhapvattu.MaPhieuNhap.Value, mnhapvattu.MaKho);
                        if (sltonmoi >= 0)
                        {
                            // update vào kho hàng so luong ton moi
                            var khohang = db.KhoHangs.Where(x => x.MaKho == mnhapvattu.MaKho && x.MaPhieuNhap == mnhapvattu.MaPhieuNhap && x.MaVatTu == item.MaVatTu).FirstOrDefault();
                            khohang.SoLuongTon = sltonmoi;
                            db.KhoHangs.Update(khohang);
                        }
                        else return -1;
                    }
                    // sua trong kho
                    // cap nhap lai phieu nhap
                    nvt.TongSoLuong = mnhapvattu.TongSoLuong;
                    nvt.TongSoTien = mnhapvattu.TongSoTien;
                    db.Entry(nvtcu).CurrentValues.SetValues(nvt);
                    //db.NhapVatTus.Update(nvt);
                    transaction.Commit();
                    await db.SaveChangesAsync();
                    return 1;
                }
                catch (Exception )
                {
                    // TODO: Handle failure                     
                }
            }
            return 0;
        }
        public async Task<bool> UpdateNhapVatTuAsync(NhapVatTuViewModel nhapVatTuViewModel)
        {
            var viewModel = mp.Map<NhapVatTu>(nhapVatTuViewModel);
            db.NhapVatTus.Update(viewModel);
            await db.SaveChangesAsync();
            var result = await this.SumTongLuongTongTien(nhapVatTuViewModel.MaPhieuNhap.Value);
            return result;
        }
        public async Task<bool> SumTongLuongTongTien(int id)
        {
            int tongluong = 0;
            decimal tongtien = 0;
            var list = await db.NhapChiTiets.Where(x => x.MaPhieuNhap == id).ToListAsync();
            foreach (var item in list)
            {
                tongluong += item.SoLuong;
                tongtien += (item.SoLuong * item.DonGia);
            }
            var entity = await db.NhapVatTus.FindAsync(id);
            entity.TongSoLuong = tongluong;
            entity.TongSoTien = tongtien;
            db.Update(entity);
            return await db.SaveChangesAsync() > 0;
        }
    }
}
