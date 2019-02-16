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
    public class NuocSanXuatRepository : INuocSanXuatRepository
    {
        DataContext db;
        IMapper mp;
        public NuocSanXuatRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }

        public async Task<bool> IsDelete(int id)
        {
            var entity = await db.NuocSanXuats.FindAsync(id);
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
                        var dvt = await db.NuocSanXuats.FindAsync(item);
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

            var nsx = await db.NuocSanXuats.FindAsync(id);
            db.NuocSanXuats.Remove(nsx);
            return await db.SaveChangesAsync() > 0;

        }
        public async Task<List<NuocSanXuatViewModel>> GetAllAsync()
        {
            return await db.NuocSanXuats.Where(x => x.Status == true)
               .ProjectTo<NuocSanXuatViewModel>(mp.ConfigurationProvider)
               .ToListAsync();
        }

        public async Task<NuocSanXuatViewModel> GetByIdAsync(int id)
        {
            var nsx = await db.NuocSanXuats.FindAsync(id);
            var mnsx = mp.Map<NuocSanXuatViewModel>(nsx);
            return mnsx;
        }

        public async Task<bool> InsertAsync(NuocSanXuatViewModel mnuocsanxuat)
        {
            var nsx = mp.Map<NuocSanXuat>(mnuocsanxuat);
            await db.NuocSanXuats.AddAsync(nsx);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(NuocSanXuatViewModel mnuocsanxuat)
        {
            var entity = mp.Map<NuocSanXuat>(mnuocsanxuat);
            var model = await db.NuocSanXuats.FindAsync(mnuocsanxuat.MaNuoc);
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
            var rs = await db.NuocSanXuats.AnyAsync(x => x.TenNuoc.ToUpper().Equals(name.ToUpper().ToTrim()));
            if (rs == true)
            {
                return (await db.NuocSanXuats.FirstOrDefaultAsync(x => x.TenNuoc.ToUpper().ToTrim() == name.ToUpper().ToTrim())).MaNuoc;
            }
            else return -1;
        }
        public async Task<bool> ChangStatus(int id)
        {
            var model = await db.NuocSanXuats.FindAsync(id);
            model.Status = !model.Status;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> GetStatus(int id)
        {
            return (await db.NuocSanXuats.FindAsync(id)).Status;
        }
        public async Task<PagedList<NuocSanXuatViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from nsx in db.NuocSanXuats
                        where nsx.Status == true
                        select new NuocSanXuatViewModel
                        {
                            MaNuoc = nsx.MaNuoc,
                            TenNuoc = nsx.TenNuoc,
                            Status = nsx.Status
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => x.TenNuoc.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenNuoc.ToUpper().Contains(keyword));
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "tenNuoc":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenNuoc);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenNuoc);
                        }
                        break;
                    default:
                        break;
                }
            }

            return await PagedList<NuocSanXuatViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
