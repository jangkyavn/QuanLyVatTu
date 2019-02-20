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
    public class XuatChiTietRepository : IXuatChiTietRepository
    {
        DataContext db;
        IMapper mp;
        IKhoHangRepository _ikhohang;
        public XuatChiTietRepository(DataContext data, IMapper mapper, IKhoHangRepository ikhohang)
        {
            db = data;
            mp = mapper;
            _ikhohang = ikhohang;
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
            try
            {
                if (kh.SoLuongTon == (await db.NhapChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == maPN && x.MaVatTu == maVT)).SoLuong)
                {
                    kh.Status = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
            var xvt = await db.XuatVatTus.FirstOrDefaultAsync(x => x.MaPhieuXuat == maphieuxuat);
            var xct = db.XuatChiTiets.Where(x => x.MaPhieuNhap == mxuatchitiet.MaPhieuNhap && x.MaVatTu == mxuatchitiet.MaVatTu && mxuatchitiet.MaPhieuXuat == maphieuxuat).FirstOrDefault();
            // lay so luong cu 
            int soluongtoncu = db.KhoHangs.Where(x => x.MaPhieuNhap == mxuatchitiet.MaPhieuNhap && x.MaVatTu == mxuatchitiet.MaVatTu && x.MaKho == makho).FirstOrDefault().SoLuongTon;
            int soluongxuatcu = xct.SoLuongXuat;
            int soluongtonmoi = (soluongtoncu + soluongxuatcu) - mxuatchitiet.SoLuongXuat;
            if (soluongtonmoi >= 0)
            {
                // update tong so luong, tong tien
                xvt.TongSoLuong += mxuatchitiet.SoLuongXuat;
                xvt.TongSoLuong -= soluongxuatcu; 
                xvt.TongSoTien += mxuatchitiet.SoLuongXuat * mxuatchitiet.DonGia;
                xvt.TongSoTien -= xct.SoLuongXuat * xct.DonGia;
                //up date chi tiet
                var xuatchitiet = mp.Map<XuatChiTiet>(mxuatchitiet);
                db.Entry(xct).CurrentValues.SetValues(xuatchitiet);                
                //update sl moi trong kho
                var kh = await db.KhoHangs.FirstOrDefaultAsync(x => x.MaPhieuNhap == mxuatchitiet.MaPhieuNhap && x.MaVatTu == mxuatchitiet.MaVatTu && x.MaKho == makho);
                kh.SoLuongTon = soluongtonmoi;
                ////////
                await db.SaveChangesAsync();
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
        public async Task<bool> InsertXuatChiTiet(XuatChiTietViewModel xuatChiTietViewModel, int mapx)
        {
            var px = await db.XuatVatTus.FindAsync(mapx);
            // check sl >= item.soluong
            var slkho = await _ikhohang.GetSLTon(xuatChiTietViewModel.MaVatTu, px.MaKho, xuatChiTietViewModel.MaPhieuNhap);
            if (slkho != 0 && slkho >= xuatChiTietViewModel.SoLuongXuat)
            {
                xuatChiTietViewModel.MaPhieuXuat = mapx;
                var xct = mp.Map<XuatChiTiet>(xuatChiTietViewModel);
                await db.XuatChiTiets.AddAsync(xct);
                await db.SaveChangesAsync();
                await this.SumSLTT(xuatChiTietViewModel, mapx);              
                // set status = fales khong cho xoa nhapchitiet cua mat hang nay vi da xuat
                var mkh = new KhoHangViewModel()
                {
                    SoLuongTon = slkho - xuatChiTietViewModel.SoLuongXuat,
                    MaKho = px.MaKho,
                    MaPhieuNhap = xuatChiTietViewModel.MaPhieuNhap,
                    MaVatTu = xuatChiTietViewModel.MaVatTu,
                    Status = false
                };
                var kh = mp.Map<KhoHang>(mkh);
                db.KhoHangs.Update(kh);
                return await db.SaveChangesAsync() > 0;
            }
            else return false; // slxuat vuot qua sl trong kho
        }
        public async Task<bool> SumSLTT(XuatChiTietViewModel xuatChiTietViewModel, int mapx)
        {
            var px = await db.XuatVatTus.FindAsync(mapx);
            px.TongSoLuong += xuatChiTietViewModel.SoLuongXuat;
            px.TongSoTien += xuatChiTietViewModel.SoLuongXuat * xuatChiTietViewModel.DonGia;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<int> CheckTonTaiVTChitiet(int maphieuxuat, int maphieunhap, int mavt)
        {
            var model = await db.XuatChiTiets.FirstOrDefaultAsync(x => x.MaVatTu == mavt && x.MaPhieuNhap == maphieunhap);
            if (model != null) return model.SoLuongXuat;
            else return -1;
        }

        public async Task<decimal> GetDonGiaChiTietXuat(int maphieuxuat, int maphieunhap, int mavt)
        {
            var rs = await db.XuatChiTiets.FirstOrDefaultAsync(x => x.MaPhieuXuat == maphieuxuat && x.MaPhieuNhap == maphieunhap && x.MaVatTu == mavt);
            return rs != null ? rs.DonGia : 0;
        }
    }
}
