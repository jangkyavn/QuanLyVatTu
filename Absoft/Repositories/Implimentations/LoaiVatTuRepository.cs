using Absoft.Data;
using Absoft.Data.Entities;
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
           
                var loaivt = await db.LoaiVatTus.FindAsync(id);
                db.LoaiVatTus.Remove(loaivt);
                return await db.SaveChangesAsync() > 0;
            
        }
        public async Task<List<LoaiVatTuViewModel>> GetAllAsync()
        {
            var query = from lvt in db.LoaiVatTus
                        join hm in db.HangMucVatTus on lvt.MaHM equals hm.MaHM
                        where lvt.Status==true
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
            var loaivattu = mp.Map<LoaiVatTu>(loaivt);
            db.LoaiVatTus.Update(loaivattu);
            return (await db.SaveChangesAsync() > 0);
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
        #endregion
    }
}
