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
        public async Task<bool> InsertListAsync(List<DonViTinhViewModel> mdonvitinh)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var dvt = mp.Map<List<DonViTinh>>(mdonvitinh);
                    foreach (var item in dvt)
                    {
                        await db.DonViTinhs.AddAsync(item);
                    }
                    transaction.Commit();
                    return await db.SaveChangesAsync() > 0;
                }
                catch (Exception)
                {
                    return false;
                }                
            }            
        }

        public async Task<bool> UpdateAsync(DonViTinhViewModel mdonvitinh)
        {
            var entity = mp.Map<DonViTinh>(mdonvitinh);
            var model = await db.DonViTinhs.FindAsync(mdonvitinh.MaDVT);
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
        public async Task<bool> IsDeleteMulti(List<int> listid)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in listid)
                    {
                        var dvt = await db.DonViTinhs.FindAsync(item);
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
        public async Task<int> CheckTonTai(string name)
        {
            var rs = await db.DonViTinhs.AnyAsync(x => x.TenDVT.ToUpper().Equals(name.ToUpper().ToTrim()));
            if (rs == true)
            {
                return (await db.DonViTinhs.FirstOrDefaultAsync(x => x.TenDVT.ToUpper().ToTrim() == name.ToUpper().ToTrim())).MaDVT;
            }
            else return -1;
        }
        public async Task<bool> ChangStatus(int id)
        {
            var model = await db.DonViTinhs.FindAsync(id);
            model.Status = !model.Status;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> GetStatus(int id)
        {
            return (await db.DonViTinhs.FindAsync(id)).Status;
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
