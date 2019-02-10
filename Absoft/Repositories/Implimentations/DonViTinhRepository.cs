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
    public class DonViTinhRepository : IDonViTinhRepository
    {
        DataContext db;
        IMapper mp;
        public DonViTinhRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }
        public async Task<List<DonViTinhViewModel>> GetAllAsync()
        {
            return await db.DonViTinhs.Where(x => x.Status == true)
                .ProjectTo<DonViTinhViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
        }
        public async Task<DonViTinhViewModel> GetById(int id)
        {
            var dvt = await db.DonViTinhs.FindAsync(id);
            return mp.Map<DonViTinhViewModel>(dvt);
        }

        public async Task<bool> InsertAsync(DonViTinhViewModel mdonvitinh)
        {
            var dvt = mp.Map<DonViTinh>(mdonvitinh);
            await db.DonViTinhs.AddAsync(dvt);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(DonViTinhViewModel mdonvitinh)
        {
            var dvt = mp.Map<DonViTinh>(mdonvitinh);
            db.DonViTinhs.Update(dvt);
            return await db.SaveChangesAsync() > 0;
        }
        // ko len su dung vi anh huong den bang vattu
        public async Task<bool> DeleteAsync(int id)
        {

            var dvt = await db.DonViTinhs.FindAsync(id);
            db.DonViTinhs.Remove(dvt);
            return await db.SaveChangesAsync() > 0;

        }
        public async Task<bool> IsDelete(int id)
        {
            var dvt = await db.DonViTinhs.FindAsync(id);
            dvt.Status = false;
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<PagedList<DonViTinhViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from dvt in db.DonViTinhs
                        where dvt.Status == true
                        select new DonViTinhViewModel
                        {
                            MaDVT = dvt.MaDVT,
                            TenDVT = dvt.TenDVT,
                            Status = dvt.Status
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => x.TenDVT.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenDVT.ToUpper().Contains(keyword));
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "tenDVT":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenDVT);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenDVT);
                        }
                        break;
                    default:
                        break;
                }
            }

            return await PagedList<DonViTinhViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
