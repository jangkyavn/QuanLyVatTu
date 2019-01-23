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
    public class HangSanXuatRepository: IHangSanXuatRepository
    {
        DataContext db;
        IMapper mp;
        public HangSanXuatRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var hangsanxuat = await db.HangSanXuats.FindAsync(id);
            db.HangSanXuats.Remove(hangsanxuat);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<List<HangSanXuatViewModel>> GetAllAsync()
        {
            return await db.HangSanXuats
                .ProjectTo<HangSanXuatViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<string> GetNameByIdAsync(int id)
        {
            return (await db.HangSanXuats.FindAsync(id)).TenHang;
        }

        public async Task<bool> InsertAsync(HangSanXuatViewModel mhangsanxuat)
        {
            var hangsanxuat = mp.Map<HangSanXuat>(mhangsanxuat);
            await db.HangSanXuats.AddAsync(hangsanxuat);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(HangSanXuatViewModel mhangsanxuat)
        {
            var hangsanxuat = await db.HangSanXuats.FindAsync(mhangsanxuat.MaHang);
            hangsanxuat.TenHang = mhangsanxuat.TenHang;
            hangsanxuat.GhiChu = mhangsanxuat.GhiChu;

            return (await db.SaveChangesAsync()) > 0;
            
        }
    }
}
