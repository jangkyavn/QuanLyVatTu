using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class ThanhLyVatTuRepository : IThanhLyVatTuRepository
    {
        DataContext db;
        IMapper mp;
        IThanhLyChiTietRepository iThanhLyChiTiet;
        public ThanhLyVatTuRepository(DataContext dataContext, IMapper mapper, IThanhLyChiTietRepository IThanhLyChiTietRepository)
        {
            this.db = dataContext;
            this.mp = mapper;
            this.iThanhLyChiTiet = IThanhLyChiTietRepository;
        }

        public async Task<bool> DeleteAsync(int maPhieuThanhLy)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var tlvt = await db.ThanhLyVatTus.FindAsync(maPhieuThanhLy);
                    var listchitiet = db.ThanhLyChiTiets.Where(x => x.MaPhieuThanhLy == maPhieuThanhLy).ToList();
                    foreach (var item in listchitiet)
                    {
                        var check = await iThanhLyChiTiet.DeleteThanhLyChiTietAsync(maPhieuThanhLy, item.MaPhieuNhap, item.MaVatTu, tlvt.MaKho);
                        if (check == false)
                        {
                            return false;
                        }
                    }
                    db.ThanhLyVatTus.Remove(tlvt);
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

        public async Task<PagedList<ThanhLyVatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from tlvt in db.ThanhLyVatTus
                        join kvt in db.KhoVatTus on tlvt.MaKho equals kvt.MaKho
                        join ns in db.NhanSus on tlvt.MaNS equals ns.MaNS
                        orderby tlvt.NgayThanhLy descending
                        select new ThanhLyVatTuViewModel
                        {
                            MaPhieuThanhLy = tlvt.MaPhieuThanhLy,
                            MaKho = tlvt.MaKho,
                            MaNS = tlvt.MaNS,
                            NgayThanhLy = tlvt.NgayThanhLy,                           
                            TongSoLuong = tlvt.TongSoLuong,                             
                            Status = tlvt.Status,
                            TenKho = kvt.TenKho,
                            TenNS = ns.HoTen                                                      
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                if (DateTime.TryParseExact(keyword, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    query = query.Where(x => DateTime.Parse(x.NgayThanhLy).Day == date.Day && DateTime.Parse(x.NgayThanhLy).Month == date.Month && DateTime.Parse(x.NgayThanhLy).Year == date.Year);
                }
                else
                {
                    query = query.Where(x => x.TenKho.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenKho.ToUpper().Contains(keyword) ||
                                        x.TenNS.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenNS.ToUpper().Contains(keyword) ||        
                                        x.NgayThanhLy.Contains(keyword) ||
                                        x.TongSoLuong.ToString().Contains(keyword));
                }
            }
            if (!string.IsNullOrEmpty(pagingParams.toDate) && !string.IsNullOrEmpty(pagingParams.fromDate))
            {
                var fromDate = pagingParams.fromDate;
                var toDate = pagingParams.toDate;
                query = query.Where(x => DateTime.Parse(x.NgayThanhLy) >= DateTime.Parse(fromDate) && DateTime.Parse(x.NgayThanhLy) <= DateTime.Parse(toDate));
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
                    case "ngayThanhLy":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.NgayThanhLy);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.NgayThanhLy);
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
            return await PagedList<ThanhLyVatTuViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<int> InserThanhLyVatTu(ThanhLyVatTuViewModel thanhLyVatTuViewModel)
        {
            thanhLyVatTuViewModel.TongSoLuong = 0;            
            var entity = mp.Map<ThanhLyVatTu>(thanhLyVatTuViewModel);
            await db.ThanhLyVatTus.AddAsync(entity);
            await db.SaveChangesAsync();
            return entity.MaPhieuThanhLy;
        }

        public async Task<bool> UpdateThanhLyVatTuAsync(ThanhLyVatTuViewModel thanhLyVatTuViewModel)
        {
            var entity = mp.Map<ThanhLyVatTu>(thanhLyVatTuViewModel);
            db.ThanhLyVatTus.Update(entity);
            await db.SaveChangesAsync();
            var rs = await this.SumTongLuong(thanhLyVatTuViewModel.MaPhieuThanhLy);
            return rs;
        }

        private async Task<bool> SumTongLuong(int id)
        {
            int tongluong = 0;            
            var list = await db.ThanhLyChiTiets.Where(x => x.MaPhieuThanhLy == id).ToListAsync();
            foreach (var item in list)
            {
                tongluong += item.SoLuongThanhLy.Value;               
            }
            var entity = await db.ThanhLyVatTus.FindAsync(id);
            entity.TongSoLuong = tongluong;            
            db.Update(entity);
            return await db.SaveChangesAsync() > 0;
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
        public async Task<List<VatTuViewModel>> GetListVatTuByMaKhoAsync(int maKho)
        {
            var query = from kh in db.KhoHangs
                        where kh.MaKho == maKho
                        select kh.MaVatTu;

            var list = await (from q in query
                                join vt in db.VatTus on q equals vt.MaVatTu
                                select new VatTuViewModel
                                {
                                    MaVatTu = vt.MaVatTu,
                                    TenVT = vt.TenVT
                                }).ToListAsync();
            return list;
        }
        public async Task<ThanhLyVatTuParams> GetDetailAsync(int maPTL)
        {
            var ptl = await (from tlvt in db.ThanhLyVatTus
                            join kvt in db.KhoVatTus on tlvt.MaKho equals kvt.MaKho
                            join ns in db.NhanSus on tlvt.MaNS equals ns.MaNS
                            where tlvt.MaPhieuThanhLy == maPTL
                            select new ThanhLyVatTuViewModel
                            {
                                MaPhieuThanhLy = tlvt.MaPhieuThanhLy,
                                SoPhieuThanhLy = tlvt.SoPhieuThanhLy,
                                MaKho = tlvt.MaKho,
                                TenKho = kvt.TenKho,
                                MaNS = tlvt.MaNS,
                                TenNS = ns.HoTen,                                
                                NgayThanhLy = tlvt.NgayThanhLy,                                
                                Status = tlvt.Status,
                                TongSoLuong = tlvt.TongSoLuong                             
                            }).FirstOrDefaultAsync();

            var chiTietVM = await (from ct in db.ThanhLyChiTiets
                                   join vt in db.VatTus on ct.MaVatTu equals vt.MaVatTu
                                   join dvt in db.DonViTinhs on vt.MaDVT equals dvt.MaDVT into tmpDonViTinhs
                                   from dvt in tmpDonViTinhs.DefaultIfEmpty()
                                   where ct.MaPhieuThanhLy == maPTL
                                   select new ThanhLyChiTietViewModel
                                   {
                                       MaPhieuThanhLy = ct.MaPhieuThanhLy,
                                       MaPhieuNhap = ct.MaPhieuNhap,
                                       MaVatTu = ct.MaVatTu,
                                       TenVT = vt.TenVT,
                                       TenDVT = dvt.TenDVT,                                     
                                       SoLuongThanhLy = ct.SoLuongThanhLy,
                                       GhiChu = ct.GhiChu,
                                       DienGiai = ct.DienGiai,
                                       Status = ct.Status
                                   }).ToListAsync();

            // var mptl = mp.Map<ThanhLyVatTuViewModel>(ptl);
            return new ThanhLyVatTuParams()
            {
                mthanhlyvattu = ptl,
                listThanhlychitiet = chiTietVM
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
        public async Task<List<NhapChiTietViewModel>> GetListNhapChiTietByMaVTAsync(int maVT)
        {
            var listnct = await db.NhapChiTiets.Where(x => x.MaVatTu == maVT).ToListAsync();
            return mp.Map<List<NhapChiTietViewModel>>(listnct);
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
                                        x.MaPhieuNhap.ToString().Contains(keyword) ||
                                        x.SoLuongTon.ToString().Contains(keyword));
            }

            return await model.ToListAsync();
        }

        public async Task<ThanhLyChiTietViewModel> GetThanhLyChiTiet(int maptl, int mapn, int mavt)
        {
            var entity = await db.ThanhLyChiTiets.Where(x => x.MaPhieuThanhLy == maptl && x.MaPhieuNhap == mapn && x.MaVatTu == mavt).ToListAsync();
            var model = mp.Map<ThanhLyChiTietViewModel>(entity);
            return model;
        }
    }
}
