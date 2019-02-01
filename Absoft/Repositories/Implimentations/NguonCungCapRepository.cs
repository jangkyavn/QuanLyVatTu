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
    public class NguonCungCapRepository: INguonCungCapRepository
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
        public async Task<bool> DeleteAsync(int id)
        {
            
                var ncc = await db.NguonCungCaps.FindAsync(id);
                db.NguonCungCaps.Remove(ncc);
                return await db.SaveChangesAsync() > 0;
            
        }
        public async Task<List<NguonCungCapViewModel>> GetAllAsync()
        {
            return await db.NguonCungCaps.Where(x => x.Status == true)
                .ProjectTo<NguonCungCapViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<NguonCungCapViewModel> GetByIdAsync(int id)
        {
            return mp.Map< NguonCungCapViewModel > (await db.NguonCungCaps.FindAsync(id));
            
        }

        public async Task<bool> InsertAsync(NguonCungCapViewModel mnguoncungcap)
        {
            var ncc = mp.Map<NguonCungCap>(mnguoncungcap);
            await db.NguonCungCaps.AddAsync(ncc);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(NguonCungCapViewModel mnguoncungcap)
        {
            var ncc = mp.Map<NguonCungCap>(mnguoncungcap);
            db.NguonCungCaps.Update(ncc);
            return await db.SaveChangesAsync() > 0;
        }
    }
}
