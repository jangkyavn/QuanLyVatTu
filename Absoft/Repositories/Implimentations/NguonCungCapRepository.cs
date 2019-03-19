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
    public class NguonCungCapRepository : INguonCungCapRepository
    {
        DataContext db;
        IMapper mp;
        public NguonCungCapRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }
        public async Task<bool> IsDelete(int id)
        {
            var entity = await db.NguonCungCaps.FindAsync(id);
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
                        var dvt = await db.NguonCungCaps.FindAsync(item);
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
                var ncc = await db.NguonCungCaps.FindAsync(id);
                db.NguonCungCaps.Remove(ncc);
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
                        var dvt = await db.NguonCungCaps.FindAsync(id);
                        if (dvt != null)
                        {
                            db.NguonCungCaps.Remove(dvt);
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
        public async Task<List<NguonCungCapViewModel>> GetAllAsync()
        {
            return await db.NguonCungCaps.Where(x => x.Status == true)
                .OrderByDescending(x => x.MaNguon)
                .ProjectTo<NguonCungCapViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<NguonCungCapViewModel> GetByIdAsync(int id)
        {
            return mp.Map<NguonCungCapViewModel>(await db.NguonCungCaps.FindAsync(id));

        }

        public async Task<bool> InsertAsync(NguonCungCapViewModel mnguoncungcap)
        {
            var ncc = mp.Map<NguonCungCap>(mnguoncungcap);
            await db.NguonCungCaps.AddAsync(ncc);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(NguonCungCapViewModel mnguoncungcap)
        {
            var entity = mp.Map<NguonCungCap>(mnguoncungcap);
            var model = await db.NguonCungCaps.FindAsync(mnguoncungcap.MaNguon);
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
            var rs = await db.NguonCungCaps.AnyAsync(x => x.TenNguon.ToUpper().Equals(name.ToUpper().ToTrim()));
            if (rs == true)
            {
                return (await db.NguonCungCaps.FirstOrDefaultAsync(x => x.TenNguon.ToUpper().ToTrim() == name.ToUpper().ToTrim())).MaNguon;
            }
            else return -1;
        }
        public async Task<bool> ChangStatus(int id)
        {
            var model = await db.NguonCungCaps.FindAsync(id);
            model.Status = !model.Status;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> GetStatus(int id)
        {
            return (await db.NguonCungCaps.FindAsync(id)).Status;
        }
        public async Task<PagedList<NguonCungCapViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from ncc in db.NguonCungCaps
                          where ncc.Status == true
                          orderby ncc.MaNguon descending
                          select new NguonCungCapViewModel
                          {
                              MaNguon = ncc.MaNguon,
                              TenNguon = ncc.TenNguon,
                              GhiChu = ncc.GhiChu ?? String.Empty,
                              Status = ncc.Status,
                          };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => 
                    x.TenNguon.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    x.TenNguon.ToUpper().Contains(keyword) ||
                    x.GhiChu.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    x.GhiChu.ToUpper().Contains(keyword));
            }
            if (!string.IsNullOrEmpty(pagingParams.KeywordCol))
            {
                if (pagingParams.ColName == "tenNguon")
                    query = query.Where(x => x.TenNguon == pagingParams.KeywordCol.Trim());             
                if (pagingParams.ColName == "ghiChu")
                    query = query.Where(x => x.GhiChu == pagingParams.KeywordCol.Trim());
            }
            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "tenNguon":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenNguon);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenNguon);
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

            return await PagedList<NguonCungCapViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
