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
        public async Task<int> UpdateNhapChiTietAsync(NhapChiTietViewModel mnhapchitiet, int maphieunhap, int makho)
        {
            // lay so luong cu 
            int soluongtoncu = db.KhoHangs.Where(x => x.MaPhieuNhap == maphieunhap && x.MaVatTu == mnhapchitiet.MaVatTu).FirstOrDefault().SoLuongTon;
            int soluongnhapcu = db.NhapChiTiets.Where(x => x.MaPhieuNhap == maphieunhap && x.MaVatTu == mnhapchitiet.MaVatTu).FirstOrDefault().SoLuong;
            int soluongtonmoi = (soluongtoncu + mnhapchitiet.SoLuong) - soluongnhapcu;
            if (soluongtonmoi >= 0)
            {
                try
                {
                    var nct = db.NhapChiTiets.FirstOrDefault(x=>x.MaPhieuNhap==maphieunhap&&x.MaVatTu==mnhapchitiet.MaVatTu); 
                    var nhapChiTiet = mp.Map<NhapChiTiet>(mnhapchitiet);
                    db.Entry(nct).CurrentValues.SetValues(nhapChiTiet);                                   
                }
                catch (Exception e)
                {

                    throw e;
                }
                await db.SaveChangesAsync();
                return soluongtonmoi;                
            }
            else return -1;
        }
        public async Task<bool> DeleteNhapChiTietAsync(NhapChiTietViewModel mnhapchitiet, int maphieunhap, int makho)
        {
            var khohang = db.KhoHangs.FirstOrDefault(x => x.MaKho == makho && x.MaPhieuNhap == maphieunhap && x.MaVatTu == mnhapchitiet.MaVatTu);
            if (khohang.Status == false)
            {
                return false;
            }
            else
            {
                var nct = mp.Map<NhapChiTiet>(mnhapchitiet);
                db.NhapChiTiets.Remove(nct);
                db.KhoHangs.Remove(khohang);
                return await db.SaveChangesAsync() > 0;
            }
        }
    }
}
