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
    public class DonViTinhRepository : IDonViTinhRepository
    {
        DataContext db;
        IMapper mp;
        public DonViTinhRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }

        public async Task<List<DonViTinhViewModel>> GetAllAsync()
        {
            return await db.DonViTinhs
                .ProjectTo<DonViTinhViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<DonViTinhViewModel> GetById(int id)
        {
            var dvt = await db.DonViTinhs.FindAsync(id);
            return mp.Map<DonViTinhViewModel>(dvt);
        }

        public async Task<bool> InsertAsync(DonViTinhViewModel mdonvitinh)
        {
            var dvt = mp.Map<DonViTinh>(mdonvitinh);
            await db.DonViTinhs.AddAsync(dvt);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(DonViTinhViewModel mdonvitinh)
        {
            var dvt = mp.Map<DonViTinh>(mdonvitinh);
            db.DonViTinhs.Update(dvt);
            return await db.SaveChangesAsync()>0;
        }
        // ko len su dung vi anh huong den bang vattu
        public async Task<bool> DeleteAsync(int id)
        {
            var dvt = await db.DonViTinhs.FindAsync(id);
            db.DonViTinhs.Remove(dvt);
            return await db.SaveChangesAsync() > 0;
        }
    }
}
