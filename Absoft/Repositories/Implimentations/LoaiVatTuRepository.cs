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
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class LoaiVatTuRepository : ILoaiVatTuRepository
    {
        DataContext db;
        IMapper mp;
        public LoaiVatTuRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var loaivt = await db.LoaiVatTus.FindAsync(id);
                db.LoaiVatTus.Remove(loaivt);
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
                        var dvt = await db.LoaiVatTus.FindAsync(id);
                        if (dvt != null)
                        {
                            db.LoaiVatTus.Remove(dvt);
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
        public async Task<List<LoaiVatTuViewModel>> GetAllAsync()
        {
            var query = from lvt in db.LoaiVatTus
                        join hm in db.HangMucVatTus on lvt.MaHM equals hm.MaHM
                        where lvt.Status == true
                        select new LoaiVatTuViewModel
                        {
                            MaLoaiVatTu = lvt.MaLoaiVatTu,
                            TenLoai = lvt.TenLoai,
                            GhiChu = lvt.GhiChu,
                            MaHM = lvt.MaHM,
                            TenHM = hm.TenHM
                        };

            return await query.ToListAsync();

        }
        // lay tat ca vat tu theo hang muc
        public async Task<List<LoaiVatTuViewModel>> GetListLoaiByMaHM(int maHM)
        {
            return await db.LoaiVatTus
                .Where(x => x.MaHM == maHM)
                .ProjectTo<LoaiVatTuViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<LoaiVatTuViewModel> GetByIdAsync(int id)
        {
            return mp.Map<LoaiVatTuViewModel>(await db.LoaiVatTus.FindAsync(id));
        }
        public async Task<bool> InsertAsync(LoaiVatTuViewModel loaivt)
        {
            var loai = mp.Map<LoaiVatTu>(loaivt);
            loai.MaHM = loaivt.MaHM.Value;
            await db.LoaiVatTus.AddAsync(loai);
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(LoaiVatTuViewModel loaivt)
        {
            var entity = mp.Map<LoaiVatTu>(loaivt);
            var model = await db.LoaiVatTus.FindAsync(loaivt.MaLoaiVatTu);
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
            var rs = await db.LoaiVatTus.AnyAsync(x => x.TenLoai.ToUpper().Equals(name.ToUpper().ToTrim()));
            if (rs == true)
            {
                return (await db.LoaiVatTus.FirstOrDefaultAsync(x => x.TenLoai.ToUpper().ToTrim() == name.ToUpper().ToTrim())).MaLoaiVatTu;
            }
            else return -1;
        }
        public async Task<bool> ChangStatus(int id)
        {
            var model = await db.LoaiVatTus.FindAsync(id);
            model.Status = !model.Status;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> GetStatus(int id)
        {
            return (await db.LoaiVatTus.FindAsync(id)).Status;
        }
        #region Khong nen su dung vi anh huong den bang vattu
        // xoa tat ca loai vat tu theo hang muc       
        // use transaction xoa tat ca loai vat tu theo hang muc
        public async Task<bool> DeleteAllByMaHMAsync(int maHM)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var mloaivt = await db.LoaiVatTus
                    .Where(x => x.MaHM == maHM)
                    .ProjectTo<LoaiVatTuViewModel>(mp.ConfigurationProvider)
                    .ToListAsync();
                    var loaivt = mp.Map<List<LoaiVatTu>>(mloaivt);
                    foreach (var item in loaivt)
                    {
                        db.LoaiVatTus.Remove(item);
                    }
                    var result = await db.SaveChangesAsync();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                    return result > 0;
                }
                catch (Exception)
                {
                    // TODO: Handle failure                    
                }
            }
            return false;
        }
        public async Task<bool> IsDelete(int id)
        {
            var entity = await db.LoaiVatTus.FindAsync(id);
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
                        var dvt = await db.LoaiVatTus.FindAsync(item);
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
        public async Task<PagedList<LoaiVatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from lvt in db.LoaiVatTus
                        join hm in db.HangMucVatTus on lvt.MaHM equals hm.MaHM
                        where lvt.Status == true
                        select new LoaiVatTuViewModel
                        {
                            MaLoaiVatTu = lvt.MaLoaiVatTu,
                            TenLoai = lvt.TenLoai,
                            GhiChu = lvt.GhiChu,
                            MaHM = lvt.MaHM,
                            TenHM = hm.TenHM
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => x.TenLoai.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenLoai.ToUpper().Contains(keyword) ||
                                        x.TenHM.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenHM.ToUpper().Contains(keyword) ||
                                        x.GhiChu.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.GhiChu.ToUpper().Contains(keyword));
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "tenLoai":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenLoai);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenLoai);
                        }
                        break;
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

            return await PagedList<LoaiVatTuViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
        #endregion
    }
}
