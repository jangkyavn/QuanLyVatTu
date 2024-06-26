﻿using Absoft.Data;
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
    public class HangSanXuatRepository: IHangSanXuatRepository
    {
        DataContext db;
        IMapper mp;
        public HangSanXuatRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;

        }

        public async Task<bool> IsDelete(int id)
        {
            var entity = await db.HangSanXuats.FindAsync(id);
            entity.Status = false;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> IsDeleteMulti(List<int> listid)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in listid)
                    {
                        var dvt = await db.HangSanXuats.FindAsync(item);
                        dvt.Status = false;
                    }
                    transaction.Commit();
                    return await db.SaveChangesAsync() > 0;
                }
                catch (Exception)
                {
                    // TODO: Handle failure                    
                }
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var hangsanxuat = await db.HangSanXuats.FindAsync(id);
                db.HangSanXuats.Remove(hangsanxuat);
                return await db.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }                                    
        }
        public async Task<bool> DeleteAllAsync(List<int> listId)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var id in listId)
                    {
                        var dvt = await db.HangSanXuats.FindAsync(id);
                        if (dvt != null)
                        {
                            db.HangSanXuats.Remove(dvt);
                        }
                    }
                    transaction.Commit();
                    return await db.SaveChangesAsync() > 0;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }
        public async Task<List<HangSanXuatViewModel>> GetAllAsync()
        {
            return await db.HangSanXuats.Where(x => x.Status == true)
                .OrderByDescending(x => x.MaHang)
                .ProjectTo<HangSanXuatViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<HangSanXuatViewModel> GetByIdAsync(int id)
        {
            return mp.Map<HangSanXuatViewModel>(await db.HangSanXuats.FindAsync(id));
        }

        public async Task<bool> InsertAsync(HangSanXuatViewModel mhangsanxuat)
        {
            var hangsanxuat = mp.Map<HangSanXuat>(mhangsanxuat);
            await db.HangSanXuats.AddAsync(hangsanxuat);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(HangSanXuatViewModel mhangsanxuat)
        {
            var entity = mp.Map<HangSanXuat>(mhangsanxuat);
            var model = await db.HangSanXuats.FindAsync(mhangsanxuat.MaHang);
            try
            {
                entity.Status = model.Status;
                db.Entry(model).CurrentValues.SetValues(entity);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

            }
            return false;

        }
        public async Task<int> CheckTonTai(string name)
        {
            var rs = await db.HangSanXuats.AnyAsync(x => x.TenHang.ToUpper().Equals(name.ToUpper().ToTrim()));
            if (rs == true)
            {
                return (await db.HangSanXuats.FirstOrDefaultAsync(x => x.TenHang.ToUpper().ToTrim() == name.ToUpper().ToTrim())).MaHang;
            }
            else return -1;
        }
        public async Task<bool> ChangStatus(int id)
        {
            var model = await db.HangSanXuats.FindAsync(id);
            model.Status = !model.Status;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> GetStatus(int id)
        {
            return (await db.HangSanXuats.FindAsync(id)).Status;
        }
        public async Task<PagedList<HangSanXuatViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from hsx in db.HangSanXuats
                          where hsx.Status == true
                          select new HangSanXuatViewModel
                          {
                              MaHang = hsx.MaHang,
                              TenHang = hsx.TenHang,
                              GhiChu = hsx.GhiChu ?? string.Empty,
                              Status = hsx.Status
                          };
            if (!string.IsNullOrEmpty(pagingParams.SearchValue))
            {
                if (pagingParams.SearchKey == "tenHang")
                    query = query.Where(x => x.TenHang == pagingParams.SearchValue.Trim());
                if (pagingParams.SearchKey == "ghiChu")
                    query = query.Where(x => x.GhiChu == pagingParams.SearchValue.Trim());
            }
            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => x.TenHang.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenHang.ToUpper().Contains(keyword) ||
                                        x.GhiChu.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.GhiChu.ToUpper().Contains(keyword));
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "tenHang":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenHang);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenHang);
                        }
                        break;
                    case "ghiChu":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.GhiChu);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.GhiChu);
                        }
                        break;
                    default:
                        break;
                }
            }

            return await PagedList<HangSanXuatViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
