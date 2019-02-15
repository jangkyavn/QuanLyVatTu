using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class XuatVatTuRepository : IXuatVatTuRepository
    {
        DataContext db;
        IMapper mp;
        IKhoHangRepository ikhohang;
        IXuatChiTietRepository ixuatchitiet;
        public XuatVatTuRepository(DataContext data, IMapper mapper, IKhoHangRepository IKhoHangRepository, IXuatChiTietRepository IXuatChiTietRepository)
        {
            db = data;
            mp = mapper;
            ikhohang = IKhoHangRepository;
            ixuatchitiet = IXuatChiTietRepository;
        }
        public async Task<bool> DeleteAsync(int maPX)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var xvt = await db.XuatVatTus.FindAsync(maPX);
                    var listchitiet = db.XuatChiTiets.Where(x => x.MaPhieuXuat == maPX).ToList();
                    foreach (var item in listchitiet)
                    {
                        var check = await ixuatchitiet.DeleteXuatChiTietAsync(maPX, item.MaPhieuNhap, item.MaVatTu, xvt.MaKho);
                        if (check == false)
                        {
                            return false;
                        }
                    }
                    db.XuatVatTus.Remove(xvt);
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
        public async Task<List<XuatVatTuViewModel>> GetAllAsync()
        {
            var model = from xvt in db.XuatVatTus
                        join kvt in db.KhoVatTus on xvt.MaKho equals kvt.MaKho
                        join ns in db.NhanSus on xvt.MaNS equals ns.MaNS
                        select new XuatVatTuViewModel
                        {
                            MaPhieuXuat = xvt.MaPhieuXuat,
                            MaKho = xvt.MaKho,
                            MaNS = xvt.MaNS,
                            NgayNhap = xvt.NgayNhap,
                            TongSoTien = xvt.TongSoTien,
                            TongSoLuong = xvt.TongSoLuong,
                            GhiChu = xvt.GhiChu,
                            Status = xvt.Status,
                            TenKho = kvt.TenKho,
                            TenNS = ns.HoTen
                        };
            return await model.ToListAsync();
        }
        public async Task<XuatVatTuParams> GetDetailAsync(int maPX)
        {
            var px = await db.XuatVatTus.FindAsync(maPX);
            var chiTietVM = from ct in db.XuatChiTiets
                            join vt in db.VatTus on ct.MaVatTu equals vt.MaVatTu
                            where ct.MaPhieuXuat == maPX
                            select new XuatChiTietViewModel
                            {
                                MaPhieuXuat = ct.MaPhieuXuat,
                                MaPhieuNhap = ct.MaPhieuNhap,
                                MaVatTu = ct.MaVatTu,
                                TenVT = vt.TenVT,
                                DonGia = ct.DonGia,
                                SoLuongXuat = ct.SoLuongXuat,
                                GhiChu = ct.GhiChu,
                                Status = ct.Status
                            };

            var mpx = mp.Map<XuatVatTuViewModel>(px);
            return new XuatVatTuParams()
            {
                mxuatvattu = mpx,
                listxuatchitiet = chiTietVM.ToList()
            };
        }
        public async Task<XuatVatTuParams> GetByMaPNAsync(int maPN)
        {
            var xvts = from nvt in db.NhapVatTus
                       where nvt.MaPhieuNhap == maPN
                       select new XuatVatTuViewModel
                       {
                           MaKho = nvt.MaKho,
                           NgayNhap = nvt.NgayNhap,
                           TongSoLuong = nvt.TongSoLuong,
                           TongSoTien = nvt.TongSoTien,
                           GhiChu = nvt.GhiChu
                       };
            var xvt = await xvts.FirstOrDefaultAsync();
            var xcts = from nct in db.NhapChiTiets
                       join kh in db.KhoHangs on nct.MaPhieuNhap equals kh.MaPhieuNhap
                       where nct.MaPhieuNhap == maPN && kh.MaVatTu == nct.MaVatTu
                       select new XuatChiTietViewModel
                       {
                           MaPhieuNhap = nct.MaPhieuNhap,
                           MaVatTu = nct.MaVatTu,
                           SoLuongXuat = kh.SoLuongTon,
                           DonGia = nct.DonGia,
                           GhiChu = nct.GhiChu
                       };
            var mxvt = mp.Map<XuatVatTuViewModel>(xvt);
            var mlistxuatct = mp.Map<List<XuatChiTietViewModel>>(xcts.ToList());
            return new XuatVatTuParams()
            {
                mxuatvattu = mxvt,
                listxuatchitiet = mlistxuatct
            };
        }
        public async Task<List<VatTuViewModel>> GetListVTByMaPNAsync(int maPN)
        {
            var vattu = await (from nct in db.NhapChiTiets
                               join vt in db.VatTus on nct.MaVatTu equals vt.MaVatTu
                               where nct.MaPhieuNhap == maPN
                               select new VatTu
                               {
                                   MaVatTu = nct.MaVatTu,
                                   TenVT = vt.TenVT
                               }).ToListAsync();
            var mvattu = mp.Map<List<VatTuViewModel>>(vattu);
            return mvattu;
        }
        public async Task<List<int>> GetByMaKhoAsync(int maKho)
        {
            var listpn = await (from nvt in db.NhapVatTus
                                join kh in db.KhoHangs on nvt.MaKho equals kh.MaKho
                                where kh.MaKho == maKho
                                group nvt by nvt.MaPhieuNhap into g
                                select g.Key).ToListAsync();
            return listpn;
        }
        public async Task<int> InsertAsync(XuatVatTuViewModel mxuatvt, List<XuatChiTietViewModel> listxuatchitiet)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    mxuatvt.TongSoLuong = 0;
                    mxuatvt.TongSoTien = 0;
                    // tao phieu xuat de lay mapx
                    var px = mp.Map<XuatVatTu>(mxuatvt);
                    await db.XuatVatTus.AddAsync(px);
                    var res1 = await db.SaveChangesAsync();
                    if (res1 > 0)
                    {
                        foreach (var item in listxuatchitiet)
                        {
                            // check sl >= item.soluong
                            var slkho = await ikhohang.GetSLTon(item.MaVatTu, px.MaKho, item.MaPhieuNhap);
                            if (slkho != 0 && slkho >= item.SoLuongXuat)
                            {
                                var res = await ixuatchitiet.InsertAsync(item, px.MaPhieuXuat);
                                if (res == true)
                                {
                                    mxuatvt.TongSoTien += item.DonGia * item.SoLuongXuat;
                                    mxuatvt.TongSoLuong += item.SoLuongXuat;
                                    // tru so luong trong kho = 0 thi xoa trong kho
                                    // set status = fales khong cho xoa nhapchitiet cua mat hang nay vi da xuat
                                    var mkh = new KhoHangViewModel()
                                    {
                                        SoLuongTon = slkho - item.SoLuongXuat,
                                        MaKho = px.MaKho,
                                        MaPhieuNhap = item.MaPhieuNhap,
                                        MaVatTu = item.MaVatTu,
                                        Status = false
                                    };
                                    var kh = mp.Map<KhoHang>(mkh);
                                    //if ((slkho - item.SoLuongXuat) == 0) db.KhoHangs.Remove(kh);
                                    db.KhoHangs.Update(kh);
                                }
                                else return 0;
                            }
                            else return -1; // so luog xuat vuot qua sl ton                        
                        }
                    }
                    else return 0;
                    // insert phieu xuat chi tiet
                    px.TongSoLuong = mxuatvt.TongSoLuong;
                    px.TongSoTien = mxuatvt.TongSoTien;
                    db.XuatVatTus.Update(px);
                    transaction.Commit();
                    await db.SaveChangesAsync();
                    return 1; // thành công
                }
                catch (Exception)
                {
                    // TODO: Handle failure                    
                }
            }
            return 0;// có lỗi
        }
        public async Task<int> UpdateAsync(XuatVatTuViewModel mxuatvt, List<XuatChiTietViewModel> listxuatchitiet)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var model = await db.XuatVatTus.FindAsync(mxuatvt.MaPhieuXuat);
                    mxuatvt.TongSoLuong = model.TongSoLuong;
                    mxuatvt.TongSoTien = model.TongSoTien;
                    var xvt = mp.Map<XuatVatTu>(mxuatvt);
                    var xvtcu = mp.Map<XuatVatTu>(model);
                    // sua cac truong tru tong tien, tong sl
                    db.Entry(xvtcu).CurrentValues.SetValues(xvt);
                    // db.XuatVatTus.Update(xvt);
                    // sua trong chi tiet
                    foreach (var item in listxuatchitiet)
                    {

                        var ctcu = db.XuatChiTiets.FirstOrDefault(x => x.MaPhieuNhap == item.MaPhieuNhap && x.MaPhieuXuat == item.MaPhieuXuat && x.MaVatTu == item.MaVatTu);
                        // thêm sl mới vào tổng
                        mxuatvt.TongSoTien += item.DonGia * item.SoLuongXuat;
                        mxuatvt.TongSoLuong += item.SoLuongXuat;
                        // lấy sl cũ và trừ trong tổng
                        mxuatvt.TongSoLuong -= ctcu.SoLuongXuat;
                        mxuatvt.TongSoTien -= ctcu.DonGia * ctcu.SoLuongXuat;

                        int sltonmoi = await ixuatchitiet.UpdateXuatChiTietAsync(item, mxuatvt.MaPhieuXuat, mxuatvt.MaKho);
                        if (sltonmoi >= 0)
                        {
                            // update vào kho hàng so luong ton moi
                            var khohang = db.KhoHangs.FirstOrDefault(x => x.MaKho == mxuatvt.MaKho && x.MaPhieuNhap == item.MaPhieuNhap && x.MaVatTu == item.MaVatTu);
                            khohang.SoLuongTon = sltonmoi;
                            //if (sltonmoi == 0) db.KhoHangs.Remove(khohang);
                            if (sltonmoi == (await db.NhapChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == item.MaPhieuNhap && x.MaVatTu == item.MaVatTu)).SoLuong)
                            {
                                khohang.Status = true;
                            }
                            else
                            {
                                khohang.Status = false;
                                db.KhoHangs.Update(khohang);
                            }
                        }
                        else return -1;
                    }
                    // sua trong kho
                    // cap nhap lai phieu nhap
                    xvt.TongSoLuong = mxuatvt.TongSoLuong;
                    xvt.TongSoTien = mxuatvt.TongSoTien;
                    db.Entry(xvtcu).CurrentValues.SetValues(xvt);
                    //db.XuatVatTus.Update(xvt);
                    transaction.Commit();
                    await db.SaveChangesAsync();
                    return 1;
                }
                catch (Exception)
                {
                    // TODO: Handle failure                    
                }
            }
            return 0;
        }
        public async Task<List<VatTuViewModel>> GetListVatTuByMaKhoAsync(int maKho)
        {
            var query = from kh in db.KhoHangs
                        where kh.MaKho == maKho
                        select kh.MaVatTu;

            var listpn = await (from q in query
                                join vt in db.VatTus on q equals vt.MaVatTu
                                select new VatTuViewModel
                                {
                                    MaVatTu = vt.MaVatTu,
                                    TenVT = vt.TenVT
                                }).ToListAsync();
            return listpn;
        }
        public async Task<List<NhapChiTietViewModel>> GetListNhapChiTietByMaVTAsync(int maVT)
        {
            var listnct = await db.NhapChiTiets.Where(x => x.MaVatTu == maVT).ToListAsync();
            return mp.Map<List<NhapChiTietViewModel>>(listnct);
        }
        public async Task<int> InsertXuatVatTu(XuatVatTuViewModel xuatVatTuViewModel)
        {
            xuatVatTuViewModel.TongSoLuong = 0;
            xuatVatTuViewModel.TongSoTien = 0;
            var px = mp.Map<XuatVatTu>(xuatVatTuViewModel);
            await db.XuatVatTus.AddAsync(px);
            await db.SaveChangesAsync();         
            return px.MaPhieuXuat;
        }

        public async Task<PagedList<XuatVatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from xvt in db.XuatVatTus
                        join kvt in db.KhoVatTus on xvt.MaKho equals kvt.MaKho
                        join ns in db.NhanSus on xvt.MaNS equals ns.MaNS
                        select new XuatVatTuViewModel
                        {
                            MaPhieuXuat = xvt.MaPhieuXuat,
                            MaKho = xvt.MaKho,
                            MaNS = xvt.MaNS,
                            NgayNhap = xvt.NgayNhap,
                            TongSoTien = xvt.TongSoTien,
                            TongSoLuong = xvt.TongSoLuong,
                            GhiChu = xvt.GhiChu,
                            Status = xvt.Status,
                            TenKho = kvt.TenKho,
                            TenNS = ns.HoTen,
                            ChietKhau = xvt.ChietKhau
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => x.TenKho.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenKho.ToUpper().Contains(keyword) ||
                                        x.TenNS.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenNS.ToUpper().Contains(keyword) ||
                                        x.NgayNhap.Equals(keyword) ||
                                        x.TongSoTien.ToString().Equals(keyword) ||
                                        x.TongSoLuong.ToString().Equals(keyword));
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
                    case "tenNS":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenNS);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenNS);
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
                    default:
                        break;
                }
            }

            return await PagedList<XuatVatTuViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }


        public async Task<List<KhoHangViewModel>> GetListByMaKho(int makho, string keyword)
        {
            var model = from kh in db.KhoHangs
                        join vt in db.VatTus on kh.MaVatTu equals vt.MaVatTu
                        where kh.MaKho == makho
                        select new KhoHangViewModel
                        {
                            MaKho = kh.MaKho,
                            MaPhieuNhap = kh.MaPhieuNhap,
                            MaVatTu = vt.MaVatTu,
                            TenVatTu = vt.TenVT,
                            SoLuongTon = kh.SoLuongTon
                        };

            if (!string.IsNullOrEmpty(keyword) && !keyword.Equals("null"))
            {
                keyword = keyword.ToUpper().ToTrim();

                model = model.Where(x => x.TenVatTu.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenVatTu.ToUpper().Contains(keyword) ||
                                        x.MaPhieuNhap.ToString().Equals(keyword) ||
                                        x.SoLuongTon.ToString().Equals(keyword));
            }

            return await model.ToListAsync();
        }
        public async Task<XuatChiTietViewModel> GetXuatChiTiet(int mapx, int mapn, int mavt)
        {
            var entity = await db.XuatChiTiets.Where(x => x.MaPhieuXuat == mapx && x.MaPhieuNhap == mapn && x.MaVatTu == mavt).ToListAsync();
            var model = mp.Map<XuatChiTietViewModel>(entity);
            return model;
        }

        public async Task<bool> UpdateXuatVatTuAsync(XuatVatTuViewModel xuatVatTuViewModel)
        {
            var viewModel = mp.Map<XuatVatTu>(xuatVatTuViewModel);
            db.XuatVatTus.Update(viewModel);
            await db.SaveChangesAsync();
            var rs =await this.SumTongLuongTongTien(xuatVatTuViewModel.MaPhieuXuat);
            return rs;
        }
        public async Task<bool> SumTongLuongTongTien(int id)
        {
            int tongluong = 0;
            decimal tongtien = 0;
            var list = await db.XuatChiTiets.Where(x => x.MaPhieuNhap == id).ToListAsync();
            foreach (var item in list)
            {
                tongluong += item.SoLuongXuat;
                tongtien += (item.SoLuongXuat * item.DonGia);
            }
            var entity = await db.XuatVatTus.FindAsync(id);
            entity.TongSoLuong = tongluong;
            entity.TongSoTien = tongtien;
            return await db.SaveChangesAsync() > 0;
        }
    }
}
