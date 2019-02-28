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
    public class ThanhLyChiTietRepository : IThanhLyChiTietRepository
    {
        DataContext db;
        IMapper mp;
        IKhoHangRepository _ikhohang;
        public ThanhLyChiTietRepository(DataContext dataContext, IMapper mapper, IKhoHangRepository khoHangRepository)
        {
            this.db = dataContext;
            this.mp = mapper;
            this._ikhohang = khoHangRepository;
        }

        public async Task<bool> CheckDeleteThanhLyChiTietAsync(int maPTL, int maPN, int maVT, int maKho)
        {
            var tlct = await db.ThanhLyChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == maPN && x.MaPhieuThanhLy == maPTL && x.MaVatTu == maVT);
            if (tlct != null) return true;
            else return false;
        }
        public async Task<CheckSoLuongParams> CheckSoLuongThanhLyChiTietAsync(int maphieunhap, int mvt, int sl)
        {
            int slct = (await db.NhapChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == maphieunhap && x.MaVatTu == mvt)).SoLuong.Value;
            if (sl <= slct) return new CheckSoLuongParams { Status = true, SoLuong = 1 };
            else return new CheckSoLuongParams { Status = false, SoLuong = slct };
        }
        public async Task<int> CheckTonTaiVTChitiet(int maptl, int maphieunhap, int mavt)
        {
            var model = await db.ThanhLyChiTiets.FirstOrDefaultAsync(x => x.MaVatTu == mavt && x.MaPhieuNhap == maphieunhap && x.MaPhieuThanhLy==maptl);
            if (model != null) return model.SoLuongThanhLy.Value;
            else return -1;
        }
        public async Task<bool> deleteAllThanhLyChiTiet(int maPTL, int maPN, int maVT, int maKho)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var list = await db.ThanhLyChiTiets.Where(x => x.MaPhieuThanhLy == maPTL).ToListAsync();
                    foreach (var item in list)
                    {
                        var res = await this.DeleteThanhLyChiTietAsync(item.MaPhieuThanhLy, item.MaPhieuNhap, item.MaVatTu, maKho);
                    }                  
                    transaction.Commit();
                    // await db.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    // TODO: Handle failure                         
                }
            }
            return false;           
        }
        public async Task<bool> DeleteThanhLyChiTietAsync(int maPTL, int maPN, int maVT, int maKho)
        {
            // remove thanhlychitiet
            var tlct = await db.ThanhLyChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == maPN && x.MaPhieuThanhLy == maPTL && x.MaVatTu == maVT);
            int sltl = tlct.SoLuongThanhLy.Value;
            db.ThanhLyChiTiets.Remove(tlct);
            //tru soluongtong trong ptl
            var ptl = await db.ThanhLyVatTus.FindAsync(maPTL);
            ptl.TongSoLuong -= sltl;
            // cong sl nguoc lai kho
            var kh = await db.KhoHangs.FirstOrDefaultAsync(x => x.MaKho == maKho && x.MaPhieuNhap == maPN && x.MaVatTu == maVT);
            kh.SoLuongTon += sltl;
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
        public async Task<bool> InsertThanhLyChiTiet(ThanhLyChiTietViewModel thanhLyChiTietViewModel, int maPTL)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var ptl = await db.ThanhLyVatTus.FindAsync(maPTL);
                    // check sl >= item.soluong
                    var slkho = await _ikhohang.GetSLTon(thanhLyChiTietViewModel.MaVatTu, ptl.MaKho, thanhLyChiTietViewModel.MaPhieuNhap);
                    if (slkho != 0 && slkho >= thanhLyChiTietViewModel.SoLuongThanhLy)
                    {
                        thanhLyChiTietViewModel.MaPhieuThanhLy = maPTL;
                        var tlct = mp.Map<ThanhLyChiTiet>(thanhLyChiTietViewModel);
                        await db.ThanhLyChiTiets.AddAsync(tlct);
                        await db.SaveChangesAsync();
                        await this.SumSLTT(thanhLyChiTietViewModel, maPTL);
                        // set status = fales khong cho xoa nhapchitiet cua mat hang nay vi da xuat
                        var mkh = new KhoHangViewModel()
                        {
                            SoLuongTon = slkho - thanhLyChiTietViewModel.SoLuongThanhLy,
                            MaKho = ptl.MaKho,
                            MaPhieuNhap = thanhLyChiTietViewModel.MaPhieuNhap,
                            MaVatTu = thanhLyChiTietViewModel.MaVatTu,
                            Status = false
                        };
                        var kh = mp.Map<KhoHang>(mkh);
                        db.KhoHangs.Update(kh);
                        transaction.Commit();
                        return await db.SaveChangesAsync() > 0;
                    }
                    else return false; // slthanhly vuot qua sl trong kho                    
                }
                catch (Exception)
                {
                    throw;
                }                 
            }                                  
        }
        public async Task<bool> SumSLTT(ThanhLyChiTietViewModel thanhLyChiTietViewModel, int maPTL)
        {
            var entity = await db.ThanhLyVatTus.FindAsync(maPTL);
            entity.TongSoLuong += thanhLyChiTietViewModel.SoLuongThanhLy;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<int> UpdateThanhLyChiTietAsync(ThanhLyChiTietViewModel model, int maPTL, int makho)
        {
            var tlvt = await db.ThanhLyVatTus.FirstOrDefaultAsync(x => x.MaPhieuThanhLy == maPTL);
            var tlct = db.ThanhLyChiTiets.Where(x => x.MaPhieuNhap == model.MaPhieuNhap && x.MaVatTu == model.MaVatTu && model.MaPhieuThanhLy == maPTL).FirstOrDefault();
            // lay so luong cu 
            int soluongtoncu = db.KhoHangs.Where(x => x.MaPhieuNhap == model.MaPhieuNhap && x.MaVatTu == model.MaVatTu && x.MaKho == makho).FirstOrDefault().SoLuongTon.Value;
            int soluongthanhlycu = tlct.SoLuongThanhLy.Value;
            int soluongtonmoi = (soluongtoncu + soluongthanhlycu) - model.SoLuongThanhLy.Value;
            if (soluongtonmoi >= 0)
            {
                // update tong so luong, tong tien
                tlvt.TongSoLuong += model.SoLuongThanhLy;
                tlvt.TongSoLuong -= soluongthanhlycu;                
                //up date chi tiet
                var entity = mp.Map<ThanhLyChiTiet>(model);
                db.Entry(entity).CurrentValues.SetValues(model);
                //update sl moi trong kho
                var kh = await db.KhoHangs.FirstOrDefaultAsync(x => x.MaPhieuNhap == model.MaPhieuNhap && x.MaVatTu == model.MaVatTu && x.MaKho == makho);
                kh.SoLuongTon = soluongtonmoi;
                ////////
                await db.SaveChangesAsync();
                return soluongtonmoi;
            }
            else return -1;
        }
    }
}
