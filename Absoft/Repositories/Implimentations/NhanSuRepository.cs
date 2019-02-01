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
    public class NhanSuRepository : INhanSuRepository
    {
        DataContext db;
        IMapper mp;
        public NhanSuRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }
        public async Task<bool> IsDelete(int id)
        {
            var entity = await db.NhanSus.FindAsync(id);
            entity.Status = false;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
          
                var ns = await db.NhanSus.FindAsync(id);
                db.NhanSus.Remove(ns);
                return await db.SaveChangesAsync() > 0;
           
        }
        public async Task<List<NhanSuViewModel>> GetAllAsync()
        {
            return await db.NhanSus.Where(x => x.Status == true)
               .ProjectTo<NhanSuViewModel>(mp.ConfigurationProvider)
               .ToListAsync();
        }

        public async Task<NhanSuViewModel> GetByIdAsync(int id)
        {
            var dvt = await db.NhanSus.FindAsync(id);
            var ns = mp.Map<NhanSuViewModel>(dvt);
            return ns;
        }

        public async Task<bool> InsertAsync(NhanSuViewModel mnhansu)
        {
            var ns = mp.Map<NhanSu>(mnhansu);
            await db.NhanSus.AddAsync(ns);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(NhanSuViewModel mnhansu)
        {
            var ns = mp.Map<NhanSu>(mnhansu);
            db.NhanSus.Update(ns);
            return await db.SaveChangesAsync() > 0;
        }       
    }
}
