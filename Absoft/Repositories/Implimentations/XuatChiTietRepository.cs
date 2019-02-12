﻿using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Helpers;
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
        public async Task<bool> CheckDeleteXuatChiTietAsync(int maPX, int maPN, int maVT, int maKho)
        {
            var xct = await db.XuatChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == maPN && x.MaPhieuXuat == maPX && x.MaVatTu == maVT);
            if (xct != null) return true;
            else return false;
        }
        public async Task<bool> DeleteXuatChiTietAsync(int maPX, int maPN, int maVT, int maKho)
        {
            // remove xuatchitiet
            var xct = await db.XuatChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == maPN && x.MaPhieuXuat == maPX && x.MaVatTu == maVT);
            int slx = xct.SoLuongXuat;
            decimal dgx = xct.DonGia;
            db.XuatChiTiets.Remove(xct);
            //tru soluongtong, tong gia trong px
            var px = await db.XuatVatTus.FindAsync(maPX);
            px.TongSoLuong -= slx;
            px.TongSoTien -= slx * dgx;
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
        public async Task<bool> deleteAllXuatChiTiet(int maPX, int maPN, int maVT, int maKho)
        {
            var list = await db.XuatChiTiets.Where(x => x.MaPhieuXuat == maPX).ToListAsync();
            foreach (var item in list)
            {
                var res = await this.DeleteXuatChiTietAsync(item.MaPhieuXuat, item.MaPhieuNhap, item.MaVatTu, maKho);
            }
            return true;
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
        public async Task<CheckSoLuongParams> CheckSoLuongXuatChiTietAsync(int maphieunhap, int mvt, int sl)
        {
            int slct = (await db.NhapChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == maphieunhap && x.MaVatTu == mvt)).SoLuong;
            if (sl <= slct) return new CheckSoLuongParams { Status = true, SoLuong = 1 };
            else return new CheckSoLuongParams { Status = false, SoLuong = slct };
        }

        public Task<bool> InsertXuatChiTiet(XuatChiTietViewModel xuatChiTietViewModel, int mapx)
        {
            throw new NotImplementedException();
        }
    }
}
