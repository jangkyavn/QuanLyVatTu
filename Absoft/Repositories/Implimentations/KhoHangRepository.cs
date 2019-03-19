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
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class KhoHangRepository : IKhoHangRepository
    {
        DataContext db;
        IMapper mp;
        public KhoHangRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }
        public async Task<List<KhoHangViewModel>> GetAllAsync()
        {
            var query = from kh in db.KhoHangs
                        join kvt in db.KhoVatTus on kh.MaKho equals kvt.MaKho
                        join vt in db.VatTus on kh.MaVatTu equals vt.MaVatTu
                        orderby kh.MaKho descending
                        select new KhoHangViewModel
                        {
                            MaKho = kh.MaKho,
                            MaPhieuNhap = kh.MaPhieuNhap,
                            MaVatTu = kh.MaVatTu,
                            SoLuongTon = kh.SoLuongTon,
                            TenKho = kvt.TenKho,
                            TenVatTu = vt.TenVT
                        };
            return await query.ToListAsync();
        }
        public async Task<List<KhoHangViewModel>> GetByMaKho(int id)
        {
            return await db.KhoHangs
                .Where(x => x.MaKho == id)
                .ProjectTo<KhoHangViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
        }
        public async Task<List<KhoHangViewModel>> GetByMaPhieuNhap(int id)
        {
            return await db.KhoHangs
               .Where(x => x.MaKho == id)
               .ProjectTo<KhoHangViewModel>(mp.ConfigurationProvider)
               .ToListAsync();
        }
        public async Task<List<KhoHangViewModel>> GetByMaVatTu(int id)
        {
            return await db.KhoHangs
               .Where(x => x.MaKho == id)
               .ProjectTo<KhoHangViewModel>(mp.ConfigurationProvider)
               .ToListAsync();
        }
        public async Task<int> GetTongTon(int maVT, int maKho)
        {
            if (maVT != 0 && maKho != 0)
            {
                var tongton = await db.KhoHangs.Where(x => x.MaVatTu == maVT && x.MaKho == maKho).SumAsync(x => x.SoLuongTon.Value);
                return tongton;
            }
            else if (maKho == 0 && maVT != 0)
            {
                var tongton = await db.KhoHangs.Where(x => x.MaVatTu == maVT).SumAsync(x => x.SoLuongTon.Value);
                return tongton;
            }
            else return 0;
        }
        public async Task<int> GetSLTon(int maVT, int maKho, int maPN)
        {
            if (maVT != 0 && maKho != 0 && maPN != 0)
            {
                var tongton = await db.KhoHangs.Where(x => x.MaVatTu == maVT && x.MaKho == maKho && x.MaPhieuNhap == maPN).SumAsync(x => x.SoLuongTon.Value);
                return tongton;
            }
            else return 0;
        }
        public async Task<bool> InsertAsync(KhoHangViewModel mkhohang)
        {
            try
            {
                var kh = mp.Map<KhoHang>(mkhohang);
                await db.KhoHangs.AddAsync(kh);
                return await db.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> DeleteAsync(KhoHangViewModel mkhohang)
        {
            var kh = mp.Map<KhoHang>(mkhohang);
            db.KhoHangs.Remove(kh);
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(KhoHangViewModel mkhohang)
        {
            var kh = mp.Map<KhoHang>(mkhohang);
            db.KhoHangs.Update(kh);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> TruSlXuat(KhoHangViewModel mkhohang)
        {
            try
            {
                var kh = mp.Map<KhoHang>(mkhohang);
                kh.SoLuongTon -= mkhohang.SoLuongTon;
                db.KhoHangs.Update(kh);
                return await db.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetTotalCountAsync()
        {
            var count = await db.KhoHangs.SumAsync(x => x.SoLuongTon.Value);
            return count;
        }

        public async Task<PagedList<KhoHangViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from kh in db.KhoHangs
                        join kvt in db.KhoVatTus on kh.MaKho equals kvt.MaKho
                        join vt in db.VatTus on kh.MaVatTu equals vt.MaVatTu
                        orderby kh.MaKho descending
                        select new KhoHangViewModel
                        {
                            MaKho = kh.MaKho,
                            MaPhieuNhap = kh.MaPhieuNhap,
                            MaVatTu = kh.MaVatTu,
                            SoLuongTon = kh.SoLuongTon,
                            TenKho = kvt.TenKho,
                            TenVatTu = vt.TenVT
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => x.TenKho.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenKho.ToUpper().Contains(keyword) ||
                                        x.TenVatTu.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenVatTu.ToUpper().Contains(keyword) ||
                                        x.MaPhieuNhap.ToString().Contains(keyword) ||
                                        x.SoLuongTon.ToString().Contains(keyword));
            }
            if (!string.IsNullOrEmpty(pagingParams.KeywordCol))
            {
                if (pagingParams.ColName == "tenKho")
                    query = query.Where(x => x.TenKho == pagingParams.KeywordCol.Trim());
                if (pagingParams.ColName == "tenVatTu")
                    query = query.Where(x => x.TenVatTu == pagingParams.KeywordCol.Trim());
                if (pagingParams.ColName == "maPhieuNhap")
                    query = query.Where(x => x.MaPhieuNhap == int.Parse(pagingParams.KeywordCol.Trim()));
                if (pagingParams.ColName == "soLuongTon")
                    query = query.Where(x => x.MaPhieuNhap == int.Parse(pagingParams.KeywordCol.Trim()));
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
    }
}
