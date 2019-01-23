using Absoft.Data;
using Absoft.Data.Entities;
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
        public async Task<bool> DeleteAsync(int id)
        {
            var khovt = await db.KhoVatTus.FindAsync(id);
            db.KhoVatTus.Remove(khovt);
            return (await db.SaveChangesAsync() > 0);
        }

        public async Task<List<KhoVatTuViewModel>> GetAllAsync()
        {
            return await db.KhoVatTus
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
            var kvt = await db.KhoVatTus.FindAsync(mkhovattu.MaKho);
            kvt.TenKho = mkhovattu.TenKho;
            kvt.DiaChi = mkhovattu.DiaChi;
            kvt.DienThoai = mkhovattu.DienThoai;
            kvt.GhiChu = mkhovattu.GhiChu;
            return await db.SaveChangesAsync() > 0;
        }
    }
}
