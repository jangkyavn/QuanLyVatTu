using Absoft.Data;
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
            int soluongtoncu = db.KhoHangs.Where(x => x.MaPhieuNhap == maphieunhap && x.MaVatTu == mnhapchitiet.MaVatTu && x.MaKho == makho).FirstOrDefault().SoLuongTon;
            int soluongnhapcu = db.NhapChiTiets.Where(x => x.MaPhieuNhap == maphieunhap && x.MaVatTu == mnhapchitiet.MaVatTu).FirstOrDefault().SoLuong;
            int soluongtonmoi = (soluongtoncu + mnhapchitiet.SoLuong) - soluongnhapcu;
            if (soluongtonmoi >= 0)
            {
                try
                {
                    var nct = db.NhapChiTiets.FirstOrDefault(x => x.MaPhieuNhap == maphieunhap && x.MaVatTu == mnhapchitiet.MaVatTu);
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
        public async Task<bool> DeleteNhapChiTietAsync(int mapn, int mavt, int makho)
        {
            //var check =await this.CheckStatus(mapn, mavt, makho);
            var khohang = await db.KhoHangs.FirstOrDefaultAsync(x => x.MaKho == makho && x.MaPhieuNhap == mapn && x.MaVatTu == mavt);
            if (khohang.Status == false)
            {
                return false;
            }
            else
            {
                var mnhapchitiet = await db.NhapChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == mapn && x.MaVatTu == mavt);
                var nvt = await db.NhapVatTus.FindAsync(mapn);
                nvt.TongSoLuong -= mnhapchitiet.SoLuong;
                nvt.TongSoTien -= mnhapchitiet.SoLuong * mnhapchitiet.DonGia;
                db.NhapChiTiets.Remove(mnhapchitiet);
                db.KhoHangs.Remove(khohang);
                // check count chi tiet pn ==0 thì xóa pn
                //var coutchitiet = await db.NhapChiTiets.CountAsync(x=>x.MaPhieuNhap == mapn);
                //if (coutchitiet == 0) db.NhapVatTus.Remove(await db.NhapVatTus.FindAsync(mapn));
                //
                return await db.SaveChangesAsync() > 0;
            }
        }
        public async Task<bool> CheckDeleteNhapChiTietAsync(int mapn, int mavt, int makho)
        {
            var mnhapchitiet = await db.NhapChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == mapn && x.MaVatTu == mavt);
            if (mnhapchitiet != null)
            {
                return true;
            }
            else return false;
        }
        public async Task<CheckSoLuongParams> CheckSoLuongNhapChiTietAsync(int maphieunhap, int makho, int mavt, int sl)
        {
            if (await this.CheckStatus(maphieunhap, mavt, makho) == true)
            {
                return new CheckSoLuongParams { Status= true, SoLuong=1};
            }
            else
            {
                int soluongtoncu =(await db.KhoHangs.Where(x => x.MaPhieuNhap == maphieunhap && x.MaVatTu == mavt && x.MaKho == makho).FirstOrDefaultAsync()).SoLuongTon;
                int soluongnhapcu =(await db.NhapChiTiets.Where(x => x.MaPhieuNhap == maphieunhap && x.MaVatTu == mavt).FirstOrDefaultAsync()).SoLuong;
                int soluongtonmoi = (soluongtoncu + sl) - soluongnhapcu;
                if (soluongtonmoi >= 0) return new CheckSoLuongParams { Status = true, SoLuong = 1 };
                else return new CheckSoLuongParams { Status = false, SoLuong = soluongnhapcu - soluongtoncu };
            }
        }
        public async Task<bool> CheckStatus(int mapn, int mavt, int makho)
        {
            var khohang = await db.KhoHangs.FirstOrDefaultAsync(x => x.MaKho == makho && x.MaPhieuNhap == mapn && x.MaVatTu == mavt);
            return (khohang.Status);
        }
        public async Task<bool> removeallNhapchitiet(int mapn, int makho)
        {
            var list = await db.NhapChiTiets.Where(x => x.MaPhieuNhap == mapn).ToListAsync();
            foreach (var item in list)
            {
                var rs = await this.DeleteNhapChiTietAsync(mapn, item.MaVatTu, makho);
            }
            return true;
        }
    }
}
