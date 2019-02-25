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
    public class HangMucVatTuRepository : IHangMucVatTuRepository
    {
        DataContext db;
        IMapper mp;
        public HangMucVatTuRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }       
        public async Task<List<HangMucVatTuViewModel>> GetAllAsync()
        {
            return await db.HangMucVatTus.Where(x=>x.Status==true)
                .ProjectTo<HangMucVatTuViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<HangMucVatTuViewModel> GetByIdAsync(int id)
        {
            return ( mp.Map<HangMucVatTuViewModel>(await db.HangMucVatTus.FindAsync(id)));
        }
        public async Task<bool> IsDeleteMulti(List<int> listid)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in listid)
                    {
                        var dvt = await db.HangMucVatTus.FindAsync(item);
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
        public async Task<bool> InsertAsync(HangMucVatTuViewModel mhangmucvattu)
        {
            var hangmucvattu = mp.Map<HangMucVatTu>(mhangmucvattu);
            await db.HangMucVatTus.AddAsync(hangmucvattu);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(HangMucVatTuViewModel mhangmucvattu)
        {
            var entity = mp.Map<HangMucVatTu>(mhangmucvattu);
            var model = await db.HangMucVatTus.FindAsync(mhangmucvattu.MaHM);
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
        // khong len su dung vi anh huong den ban loai vat tu
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var hangmucvt = await db.HangMucVatTus.FindAsync(id);
                db.HangMucVatTus.Remove(hangmucvt);
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
                        var dvt = await db.HangMucVatTus.FindAsync(id);
                        if (dvt != null)
                        {
                            db.HangMucVatTus.Remove(dvt);
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
        public async Task<bool> IsDelete(int id)
        {
            var entity = await db.HangMucVatTus.FindAsync(id);
            entity.Status = false;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<int> CheckTonTai(string name)
        {
            var rs = await db.HangMucVatTus.AnyAsync(x => x.TenHM.ToUpper().Equals(name.ToUpper().ToTrim()));
            if (rs == true)
            {
                return (await db.HangMucVatTus.FirstOrDefaultAsync(x => x.TenHM.ToUpper().ToTrim() == name.ToUpper().ToTrim())).MaHM;
            }
            else return -1;
        }
        public async Task<bool> ChangStatus(int id)
        {
            var model = await db.HangMucVatTus.FindAsync(id);
            model.Status = !model.Status;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> GetStatus(int id)
        {
            return (await db.HangMucVatTus.FindAsync(id)).Status;
        }
        public async Task<PagedList<HangMucVatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from hmvt in db.HangMucVatTus
                        where hmvt.Status == true
                        select new HangMucVatTuViewModel
                        {
                            MaHM = hmvt.MaHM,
                            TenHM = hmvt.TenHM,
                            GhiChu = hmvt.GhiChu ?? string.Empty,
                            Status = hmvt.Status
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => x.TenHM.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenHM.ToUpper().Contains(keyword) ||
                                        x.GhiChu.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.GhiChu.ToUpper().Contains(keyword));
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "tenHM":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenHM);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenHM);
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

            return await PagedList<HangMucVatTuViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
