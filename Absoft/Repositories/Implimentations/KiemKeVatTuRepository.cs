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
    public class KiemKeVatTuRepository : IKiemKeVatTuRepository
    {
        DataContext db;
        IMapper mp;
        IKiemKeChiTietRepository _IKiemKeChiTietRepository;
        public KiemKeVatTuRepository(DataContext DataContext, IMapper IMapper, IKiemKeChiTietRepository IKiemKeChiTietRepository)
        {
            db = DataContext;
            mp = IMapper;
            _IKiemKeChiTietRepository = IKiemKeChiTietRepository;
        }
        public async Task<bool> DeleteAsync(int maPhieuKiemKe)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var kkvt = await db.KiemKeVatTus.FindAsync(maPhieuKiemKe);
                    var listchitiet = db.KiemKeChiTiets.Where(x => x.MaPhieuKiemKe == maPhieuKiemKe).ToList();
                    foreach (var item in listchitiet)
                    {
                        var check = await _IKiemKeChiTietRepository.DeleteKiemKeChiTietAsync(maPhieuKiemKe, item.MaPhieuNhap, item.MaVatTu, kkvt.MaKho);
                        if (check == false)
                        {
                            return false;
                        }
                    }
                    db.KiemKeVatTus.Remove(kkvt);
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
        public async Task<bool> DisablePnPxPtl(string ngayKiemKe)
        {
            var listNhap = await db.NhapVatTus.Where(x => DateTime.Parse(x.NgayNhap) <= DateTime.Parse(ngayKiemKe)).ToListAsync();
            foreach (var iNhap in listNhap)
            {
                iNhap.Status = false;
            }
            var listXuat = await db.XuatVatTus.Where(x => DateTime.Parse(x.NgayNhap) <= DateTime.Parse(ngayKiemKe)).ToListAsync();
            foreach (var iXuat in listXuat)
            {
                iXuat.Status = false;
            }
            var listThanhLy = await db.ThanhLyVatTus.Where(x => DateTime.Parse(x.NgayThanhLy) <= DateTime.Parse(ngayKiemKe)).ToListAsync();
            foreach (var iTl in listThanhLy)
            {
                iTl.Status = false;
            }
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> UnablePnPxPtl(string ngayKiemKe)
        {
            var listNhap = await db.NhapVatTus.Where(x => DateTime.Parse(x.NgayNhap) <= DateTime.Parse(ngayKiemKe)).ToListAsync();
            foreach (var iNhap in listNhap)
            {
                iNhap.Status = true;
            }
            var listXuat = await db.XuatVatTus.Where(x => DateTime.Parse(x.NgayNhap) <= DateTime.Parse(ngayKiemKe)).ToListAsync();
            foreach (var iXuat in listXuat)
            {
                iXuat.Status = true;
            }
            var listThanhLy = await db.ThanhLyVatTus.Where(x => DateTime.Parse(x.NgayThanhLy) <= DateTime.Parse(ngayKiemKe)).ToListAsync();
            foreach (var iTl in listThanhLy)
            {
                iTl.Status = true;
            }
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<PagedList<KiemKeVatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from kkvt in db.KiemKeVatTus
                        join kvt in db.KhoVatTus on kkvt.MaKho equals kvt.MaKho
                        join ns in db.NhanSus on kkvt.MaNS equals ns.MaNS
                        orderby kkvt.NgayKiemKe descending
                        select new KiemKeVatTuViewModel
                        {
                            MaPhieuKiemKe = kkvt.MaPhieuKiemKe,
                            MaKho = kkvt.MaKho,
                            MaNS = kkvt.MaNS,
                            NgayKiemKe = kkvt.NgayKiemKe,
                            TongTheoDoi = kkvt.TongTheoDoi,
                            TongThucTon = kkvt.TongThucTon,
                            Status = kkvt.Status,
                            TenKho = kvt.TenKho,
                            TenNS = ns.HoTen
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                if (DateTime.TryParseExact(keyword, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    query = query.Where(x => DateTime.Parse(x.NgayKiemKe).Day == date.Day && DateTime.Parse(x.NgayKiemKe).Month == date.Month && DateTime.Parse(x.NgayKiemKe).Year == date.Year);
                }
                else
                {
                    query = query.Where(x => x.TenKho.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenKho.ToUpper().Contains(keyword) ||
                                        x.TenNS.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenNS.ToUpper().Contains(keyword) ||
                                        x.NgayKiemKe.Contains(keyword) ||
                                        x.TongTheoDoi.ToString().Contains(keyword) ||
                                        x.TongThucTon.ToString().Contains(keyword)
                                        );
                }
            }
            if (!string.IsNullOrEmpty(pagingParams.toDate) && !string.IsNullOrEmpty(pagingParams.fromDate))
            {
                var fromDate = pagingParams.fromDate;
                var toDate = pagingParams.toDate;
                query = query.Where(x => DateTime.Parse(x.NgayKiemKe) >= DateTime.Parse(fromDate) && DateTime.Parse(x.NgayKiemKe) <= DateTime.Parse(toDate));
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
                    case "ngayKiemKe":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.NgayKiemKe);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.NgayKiemKe);
                        }
                        break;
                    case "tongTheoDoi":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TongTheoDoi);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TongTheoDoi);
                        }
                        break;
                    case "tongThucTon":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TongThucTon);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TongThucTon);
                        }
                        break;
                    default:
                        break;
                }
            }
            return await PagedList<KiemKeVatTuViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<int> InsertAsync(KiemKeVatTuViewModel model)
        {
            model.TongTheoDoi = 0;
            model.TongThucTon = 0;
            var entity = mp.Map<KiemKeVatTu>(model);
            await db.KiemKeVatTus.AddAsync(entity);
            await db.SaveChangesAsync();
            return entity.MaPhieuKiemKe;
        }
        public async Task<bool> UpdateAsync(KiemKeVatTuViewModel model)
        {
            var entity = mp.Map<KiemKeVatTu>(model);
            db.KiemKeVatTus.Update(entity);
            await db.SaveChangesAsync();
            var rs = await SumTongLuong(model.MaPhieuKiemKe);
            return rs;
        }
        private async Task<bool> SumTongLuong(int id)
        {
            int tongtheodoi = 0;
            int tongthucton = 0;
            var list = await db.KiemKeChiTiets.Where(x => x.MaPhieuKiemKe == id).ToListAsync();
            foreach (var item in list)
            {
                tongtheodoi += item.SoLuongTheoDoi.Value;
                tongthucton += item.SoLuongThucTon.Value;
            }
            var entity = await db.KiemKeVatTus.FindAsync(id);
            entity.TongTheoDoi = tongtheodoi;
            entity.TongThucTon = tongthucton;
            db.Update(entity);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<KiemKeVatTuViewModel> GetById(int maPKK)
        {
            var entity = await db.KiemKeVatTus.FindAsync(maPKK);
            var model = mp.Map<KiemKeVatTuViewModel>(entity);
            return model;
        }

        public async Task<PagedList<KhoHangViewModel>> GetListKho(PagingParams pagingParams, int? maKho, int? maPN, int? maVT, int? maPKK, bool status)
        {
            var query = from kh in db.KhoHangs
                        join vt in db.VatTus on kh.MaVatTu equals vt.MaVatTu
                        join nvt in db.NhapVatTus on kh.MaPhieuNhap equals nvt.MaPhieuNhap
                        where (kh.MaKho == maKho.Value)
                        select new KhoHangViewModel
                        {
                            MaKho = kh.MaKho,
                            MaPhieuNhap = kh.MaPhieuNhap,
                            MaVatTu = vt.MaVatTu,
                            TenVatTu = vt.TenVT,
                            SoLuongTon = kh.SoLuongTon,
                            NgayNhap = nvt.NgayNhap,
                            Inserted = (from kkct in db.KiemKeChiTiets
                                        join kkvt in db.KiemKeVatTus on kkct.MaPhieuKiemKe equals kkvt.MaPhieuKiemKe
                                        where kkct.MaPhieuKiemKe == maPKK && kkct.MaPhieuNhap == kh.MaPhieuNhap && kkct.MaVatTu == vt.MaVatTu &&    kkvt.MaKho == maKho
                                        select kkct).Any()
                        };
            if (maPN != null) query = query.Where(x => x.MaPhieuNhap == maPN);
            if (maVT != null) query = query.Where(x => x.MaVatTu == maVT);
            if (status == true)
            {
                query = query.Where(x => x.SoLuongTon >= 0);
            }
            else
            {
                query = query.Where(x => x.SoLuongTon > 0);
            }


            string keyword = pagingParams.Keyword;
            if (!string.IsNullOrEmpty(keyword) && !keyword.Equals("null"))
            {
                keyword = keyword.ToUpper().ToTrim();

                query = query.Where(x => x.TenVatTu.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenVatTu.ToUpper().Contains(keyword) ||
                                        x.MaPhieuNhap.ToString().Contains(keyword) ||
                                        x.SoLuongTon.ToString().Contains(keyword));
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "tenVatTu":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenVatTu);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenVatTu);
                        }
                        break;
                    case "maPhieuNhap":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.MaPhieuNhap);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.MaPhieuNhap);
                        }
                        break;
                    case "soLuongTon":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.SoLuongTon);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.SoLuongTon);
                        }
                        break;
                    default:
                        break;
                }
            }
            return await PagedList<KhoHangViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
        public async Task<KiemKeVatTuParams> GetDetailAsync(int maPKK)
        {
            var queryKiemKeVT = await (from kkvt in db.KiemKeVatTus
                                       join kvt in db.KhoVatTus on kkvt.MaKho equals kvt.MaKho
                                       join ns in db.NhanSus on kkvt.MaNS equals ns.MaNS
                                       where kkvt.MaPhieuKiemKe == maPKK
                                       select new KiemKeVatTuViewModel
                                       {
                                           MaPhieuKiemKe = kkvt.MaPhieuKiemKe,
                                           SoPhieuKiemKe = kkvt.SoPhieuKiemKe,
                                           MaKho = kkvt.MaKho,
                                           MaNS = kkvt.MaNS,
                                           NgayKiemKe = kkvt.NgayKiemKe,
                                           TongTheoDoi = kkvt.TongTheoDoi,
                                           TongThucTon = kkvt.TongThucTon,
                                           Status = kkvt.Status,
                                           TenKho = kvt.TenKho,
                                           TenNS = ns.HoTen
                                       }).FirstOrDefaultAsync();

            var listkkct = await (from ct in db.KiemKeChiTiets
                                  join vt in db.VatTus on ct.MaVatTu equals vt.MaVatTu
                                  where ct.MaPhieuKiemKe == maPKK
                                  select new KiemKeChiTietViewModel
                                  {
                                      MaPhieuKiemKe = ct.MaPhieuKiemKe,
                                      MaPhieuNhap = ct.MaPhieuNhap,
                                      MaVatTu = ct.MaVatTu,
                                      SoLuongTheoDoi = ct.SoLuongTheoDoi,
                                      SoLuongThucTon = ct.SoLuongThucTon,
                                      SoLuongKiemKe = ct.SoLuongKiemKe,
                                      GhiChu = ct.GhiChu,
                                      Status = ct.Status,
                                      TenVatTu = vt.TenVT

                                  }).ToListAsync();

            return new KiemKeVatTuParams()
            {
                mKiemKeVatTu = queryKiemKeVT,
                listKiemKeChiTiet = listkkct
            };
        }
    }
}
