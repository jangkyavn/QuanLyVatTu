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
    public class VatTuRepository : IVatTuRepository
    {
        DataContext db;
        IMapper mp;
        public VatTuRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var vt = await db.VatTus.FindAsync(id);
            db.VatTus.Remove(vt);
            return await db.SaveChangesAsync() > 0;
        }
        // delete all by maloaivt use transaction 
        public async Task<bool> DeleteByMaLoaiVTAsync(int MaloaiVT)
        {
            using (var transaction = db.Database.BeginTransaction())
                try
                {
                    var mvt = await db.VatTus
                .Where(x => x.MaLoaiVatTu == MaloaiVT)
                .ProjectTo<VatTuViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
                    var vt = mp.Map<List<VatTu>>(mvt);
                    foreach (var item in vt)
                    {
                        db.VatTus.Remove(item);
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
            return false;
        }
        public async Task<List<VatTuViewModel>> GetAllAsync()
        {
            var query = from vt in db.VatTus
                        join dvt in db.DonViTinhs on vt.MaDVT equals dvt.MaDVT
                        join lvt in db.LoaiVatTus on vt.MaLoaiVatTu equals lvt.MaLoaiVatTu
                        select new VatTuViewModel
                        {
                            MaVatTu= vt.MaVatTu,
                            MaLoaiVatTu = vt.MaLoaiVatTu,
                            MaDVT =  vt.MaDVT,
                            TenVT = vt.TenVT,
                            GhiChu = vt.GhiChu,
                            TenDVT = dvt.TenDVT,
                            TenLoaiVatTu = lvt.TenLoai
                        };
            return await query.ToListAsync();
        }
        public async Task<List<VatTuViewModel>> GetByMaLoaiVTAsync(int MaloaiVT)
        {
            return await db.VatTus
               .Where(x => x.MaLoaiVatTu == MaloaiVT)
               .ProjectTo<VatTuViewModel>(mp.ConfigurationProvider)
               .ToListAsync();
        }
        public async Task<VatTuViewModel> GetByIdAsync(int id)
        {
            var vt = await db.VatTus.FindAsync(id);
            var mvt = mp.Map<VatTuViewModel>(vt);
            return mvt;
        }
        public async Task<List<VatTuViewModel>> GetByMaDVAsync(int MADV)
        {
            return await db.VatTus
              .Where(x => x.MaDVT == MADV)
              .ProjectTo<VatTuViewModel>(mp.ConfigurationProvider)
              .ToListAsync();
        }
        public async Task<bool> InsertAsync(VatTuViewModel mvattu)
        {
            var vt = mp.Map<VatTu>(mvattu);
            await db.VatTus.AddAsync(vt);
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(VatTuViewModel mvattu)
        {
            var vt = await db.VatTus.FindAsync(mvattu.MaVatTu);
            vt.TenVT = mvattu.TenVT;
            vt.MaLoaiVatTu = mvattu.MaLoaiVatTu;
            vt.MaDVT = mvattu.MaDVT;
            return await db.SaveChangesAsync() > 0;
        }
    }
}
