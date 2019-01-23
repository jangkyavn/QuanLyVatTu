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
        public async Task<bool> DeleteAsync(int id)
        {
            var ns = await db.NhanSus.FindAsync(id);
            db.NhanSus.Remove(ns);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<List<NhanSuViewModel>> GetAllAsync()
        {
            return await db.NhanSus
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
            var ns = await db.NhanSus.FindAsync(mnhansu.MaNS);            
            ns.HoTen = mnhansu.HoTen;
            ns.NgaySinh = mnhansu.NgaySinh;
            ns.QueQuan = mnhansu.QueQuan;
            ns.DanToc = mnhansu.DanToc;
            ns.TonGiao = mnhansu.TonGiao;
            ns.CapBacSHSQ = mnhansu.CapBacSHSQ;
            ns.ChucVu = mnhansu.ChucVu;
            ns.NgayNhapNguTuyenDung = mnhansu.NgayNhapNguTuyenDung;
            ns.XuatThan = mnhansu.XuatThan;
            ns.BanThan = mnhansu.BanThan;
            ns.QuaTrinhHocTap = mnhansu.QuaTrinhHocTap;
            ns.ThanNhan = mnhansu.ThanNhan;
            ns.GhiChu = mnhansu.GhiChu;
            return await db.SaveChangesAsync() > 0;
        }       
    }
}
