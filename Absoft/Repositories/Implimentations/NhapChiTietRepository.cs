using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class NhapChiTietRepository : INhapChiTietRepository
    {
        DataContext db;
        IMapper mp;
        public NhapChiTietRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }
        public async Task<bool> InsertAsync(NhapChiTietViewModel mnhapchitiet, int maphieunhap)
        {
            mnhapchitiet.MaPhieuNhap = maphieunhap;
            var nhapChiTiet = mp.Map<NhapChiTiet>(mnhapchitiet);
            await db.NhapChiTiets.AddAsync(nhapChiTiet);
            return await db.SaveChangesAsync() > 0;
        }
    }
}
