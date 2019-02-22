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
    public class KhoVatTuRepository : IKhoVatTuRepository
    {
        DataContext db;
        IMapper mp;
        public KhoVatTuRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }

        public async Task<bool> IsDelete(int id)
        {
            var entity = await db.KhoVatTus.FindAsync(id);
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
                        var dvt = await db.KhoVatTus.FindAsync(item);
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
                var khovt = await db.KhoVatTus.FindAsync(id);
                db.KhoVatTus.Remove(khovt);
                return (await db.SaveChangesAsync() > 0);
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
                        var dvt = await db.KhoVatTus.FindAsync(id);
                        if (dvt != null)
                        {
                            db.KhoVatTus.Remove(dvt);
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
        public async Task<List<KhoVatTuViewModel>> GetAllAsync()
        {
            return await db.KhoVatTus.Where(x=>x.Status==true)
                .ProjectTo<KhoVatTuViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<KhoVatTuViewModel> GetByIdAsync(int id)
        {
            return mp.Map<KhoVatTuViewModel> (await db.KhoVatTus.FindAsync(id));
            
        }

        public async Task<bool> InsertAsync(KhoVatTuViewModel mkhovattu)
        {
            var kvt = mp.Map<KhoVatTu>(mkhovattu);
            await db.KhoVatTus.AddAsync(kvt);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(KhoVatTuViewModel mkhovattu)
        {
            var entity = mp.Map<KhoVatTu>(mkhovattu);
            var model = await db.KhoVatTus.FindAsync(mkhovattu.MaKho);
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
            var rs = await db.KhoVatTus.AnyAsync(x => x.TenKho.ToUpper().Equals(name.ToUpper().ToTrim()));
            if (rs == true)
            {
                return (await db.KhoVatTus.FirstOrDefaultAsync(x => x.TenKho.ToUpper().ToTrim() == name.ToUpper().ToTrim())).MaKho;
            }
            else return -1;
        }
        public async Task<bool> ChangStatus(int id)
        {
            var model = await db.KhoVatTus.FindAsync(id);
            model.Status = !model.Status;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> GetStatus(int id)
        {
            return (await db.KhoVatTus.FindAsync(id)).Status;
        }
        public async Task<PagedList<KhoVatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from hvt in db.KhoVatTus
                        where hvt.Status == true
                        select new KhoVatTuViewModel
                          {
                              MaKho = hvt.MaKho,
                              TenKho = hvt.TenKho,
                              DienThoai = hvt.DienThoai,
                              DiaChi = hvt.DiaChi,
                              GhiChu = hvt.GhiChu,
                              Status = hvt.Status
                          };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => x.TenKho.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenKho.ToUpper().Contains(keyword) ||
                                        x.DienThoai.Contains(keyword) ||
                                        x.DiaChi.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.DiaChi.ToUpper().Contains(keyword) ||
                                        x.GhiChu.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.GhiChu.ToUpper().Contains(keyword));
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
                    case "dienThoai":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.DienThoai);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.DienThoai);
                        }
                        break;
                    case "diaChi":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.DiaChi);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.DiaChi);
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

            return await PagedList<KhoVatTuViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
