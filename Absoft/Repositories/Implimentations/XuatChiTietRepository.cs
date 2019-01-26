using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class XuatChiTietRepository : IXuatChiTietRepository
    {
        DataContext db;
        IMapper mp;
        public XuatChiTietRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }
        public async Task<bool> DeleteXuatChiTietAsync(int maPX, int maPN, int maVT, int maKho)
        {
            // remove xuatchitiet
            var xct = await db.XuatChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == maPN && x.MaPhieuXuat == maPX && x.MaVatTu == maVT);
            int slx = xct.SoLuongXuat;
            db.XuatChiTiets.Remove(xct);
            // cong sl nguoc lai kho
            var kh = await db.KhoHangs.FirstOrDefaultAsync(x => x.MaKho == maKho && x.MaPhieuNhap == maPN && x.MaVatTu == maVT);
            kh.SoLuongTon += slx;
            if (kh.SoLuongTon == (await db.NhapChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == maPN && x.MaVatTu == maVT)).SoLuong)
            {
                kh.Status = true;
            }
            db.KhoHangs.Update(kh);
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> InsertAsync(XuatChiTietViewModel mxuatchitiet, int maphieuxuat)
        {
            mxuatchitiet.MaPhieuXuat = maphieuxuat;
            var xct = mp.Map<XuatChiTiet>(mxuatchitiet);
            await db.XuatChiTiets.AddAsync(xct);
            var res = await db.SaveChangesAsync();
            return res > 0;
        }
        public async Task<int> UpdateXuatChiTietAsync(XuatChiTietViewModel mxuatchitiet, int maphieuxuat, int makho)
        {
            var xct = db.XuatChiTiets.Where(x => x.MaPhieuNhap == mxuatchitiet.MaPhieuNhap && x.MaVatTu == mxuatchitiet.MaVatTu && mxuatchitiet.MaPhieuXuat == maphieuxuat).FirstOrDefault();
            // lay so luong cu 
            int soluongtoncu = db.KhoHangs.Where(x => x.MaPhieuNhap == mxuatchitiet.MaPhieuNhap && x.MaVatTu == mxuatchitiet.MaVatTu && x.MaKho == makho).FirstOrDefault().SoLuongTon;
            int soluongxuatcu = xct.SoLuongXuat;
            int soluongtonmoi = (soluongtoncu + soluongxuatcu) - mxuatchitiet.SoLuongXuat;
            if (soluongtonmoi >= 0)
            {                
                var xuatchitiet = mp.Map<XuatChiTiet>(mxuatchitiet);
                db.Entry(xct).CurrentValues.SetValues(xuatchitiet);
                var rs = await db.SaveChangesAsync();
                return soluongtonmoi;
            }
            else return -1;
        }
    }
}
