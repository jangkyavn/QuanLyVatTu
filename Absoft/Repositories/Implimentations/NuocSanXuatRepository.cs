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
    public class NuocSanXuatRepository : INuocSanXuatRepository
    {
        DataContext db;
        IMapper mp;
        public NuocSanXuatRepository(DataContext  data, IMapper mapper)
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
            var nsx = mp.Map<NuocSanXuat>(mnuocsanxuat);
            db.NuocSanXuats.Update(nsx);
            return await db.SaveChangesAsync() > 0;
        }
    }
}
