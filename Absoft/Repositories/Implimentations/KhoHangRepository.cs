using Absoft.Data;
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
    public class KhoHangRepository : IKhoHangRepository
    {
        DataContext db;
        IMapper mp;
        public KhoHangRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }     
        public async Task<List<KhoHangViewModel>> GetAllAsync()
        {
            var query = from kh in db.KhoHangs
                        join kvt in db.KhoVatTus on kh.MaKho equals kvt.MaKho
                        join vt in db.VatTus on kh.MaVatTu equals vt.MaVatTu
                        select new KhoHangViewModel
                        {
                            MaKho = kh.MaKho,
                            MaPhieuNhap=kh.MaPhieuNhap,
                            MaVatTu= kh.MaVatTu,
                            SoLuongTon= kh.SoLuongTon,
                            TenKho= kvt.TenKho,
                            TenVatTu= vt.TenVT
                        };
            return await query.ToListAsync();
        }
        public async Task<List<KhoHangViewModel>> GetByMaKho(int id)
        {
            return await db.KhoHangs
                .Where(x => x.MaKho == id)
                .ProjectTo<KhoHangViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
        }
        public async Task<List<KhoHangViewModel>> GetByMaPhieuNhap(int id)
        {
            return await db.KhoHangs
               .Where(x => x.MaKho == id)
               .ProjectTo<KhoHangViewModel>(mp.ConfigurationProvider)
               .ToListAsync();
        }
        public async Task<List<KhoHangViewModel>> GetByMaVatTu(int id)
        {
            return await db.KhoHangs
               .Where(x => x.MaKho == id)
               .ProjectTo<KhoHangViewModel>(mp.ConfigurationProvider)
               .ToListAsync();
        }      
        public async Task<int> GetTongTon(int maVT, int maKho)
        {
            if (maVT != 0 && maKho != 0)
            {
                var tongton = await db.KhoHangs.Where(x => x.MaVatTu == maVT && x.MaKho == maKho).SumAsync(x => x.SoLuongTon);
                return tongton;
            }         
            else if (maKho == 0 && maVT != 0)
            {
                var tongton = await db.KhoHangs.Where(x => x.MaVatTu == maVT).SumAsync(x => x.SoLuongTon);
                return tongton;
            }
            else return 0;
        }       
    }
}
