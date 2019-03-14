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
    public class KiemKeChiTietRepository : IKiemKeChiTietRepository
    {
        DataContext db;
        IMapper mp;
        IKhoHangRepository _ikhohang;
        public KiemKeChiTietRepository(DataContext dataContext, IMapper mapper, IKhoHangRepository khoHangRepository)
        {
            this.db = dataContext;
            this.mp = mapper;
            this._ikhohang = khoHangRepository;
        }
        public async Task<bool> DeleteKiemKeChiTietAsync(int maPKK, int maPN, int maVT, int maKho)
        {
            // remove kiemkechitiet
            var kkct = await db.KiemKeChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == maPN && x.MaPhieuKiemKe == maPKK && x.MaVatTu == maVT);
            int sltd = kkct.SoLuongTheoDoi.Value;
            int sltt = kkct.SoLuongThucTon.Value;
            db.KiemKeChiTiets.Remove(kkct);
            //tru soluongtong trong ptl
            var kkvt = await db.KiemKeVatTus.FindAsync(maPKK);
            kkvt.TongTheoDoi -= sltd;
            kkvt.TongThucTon -= sltt;
            // tra lai so luong theo doi vao kho
            var kh = await db.KhoHangs.FirstOrDefaultAsync(x => x.MaKho == maKho && x.MaPhieuNhap == maPN && x.MaVatTu == maVT);
            kh.SoLuongTon = sltd;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> InserKiemKeChiTietAsync(KiemKeChiTietViewModel model, int maPKK, int maKho)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // insert chi tiet, update tong kho, update tongsl
                    // var pkk = await db.KiemKeVatTus.FindAsync(maPKK);            
                    var entity = mp.Map<KiemKeChiTiet>(model);
                    entity.MaPhieuKiemKe = maPKK;
                    entity.SoLuongKiemKe = entity.SoLuongThucTon - entity.SoLuongTheoDoi;
                    
                    await db.KiemKeChiTiets.AddAsync(entity);
                    await SumSL(model, maPKK);
                    var kh = await db.KhoHangs.FirstOrDefaultAsync(x => x.MaKho == maKho && x.MaPhieuNhap == model.MaPhieuNhap && x.MaVatTu == model.MaVatTu);
                    kh.SoLuongTon = model.SoLuongThucTon;

                    transaction.Commit();
                    return await db.SaveChangesAsync() > 0;                   
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public async Task<bool> UpdateKiemKeChiTietAsync(KiemKeChiTietViewModel model, int maPKK, int maKho)
        {
            var kkvt = await db.KiemKeVatTus.FindAsync(maPKK);
            
            var kkct = await db.KiemKeChiTiets.FirstOrDefaultAsync(x=>x.MaPhieuKiemKe == maPKK && x.MaPhieuNhap == model.MaPhieuNhap && x. MaVatTu == model.MaVatTu );
            kkct.SoLuongTheoDoi = kkct.SoLuongTheoDoi;
            kkct.SoLuongThucTon = model.SoLuongThucTon;
            kkct.SoLuongKiemKe = model.SoLuongThucTon - model.SoLuongTheoDoi;

            kkct.GhiChu = model.GhiChu;
            var kh = await db.KhoHangs.FirstOrDefaultAsync(x => x.MaKho == maKho && x.MaPhieuNhap == model.MaPhieuNhap && x.MaVatTu == model.MaVatTu);
            kh.SoLuongTon = model.SoLuongThucTon;

            kkvt.TongTheoDoi += model.SoLuongTheoDoi;
            kkvt.TongTheoDoi -= kkct.SoLuongTheoDoi;
            kkvt.TongThucTon += model.SoLuongThucTon;
            kkvt.TongThucTon -= kkct.SoLuongThucTon;

            return await db.SaveChangesAsync() > 0;
        }
        public async Task<int> GetSoLuongTheoDoi(int maKho, int maPN, int maVT)
        {
            var sl = (await db.KhoHangs.FirstOrDefaultAsync(x=>x.MaKho == maKho && x.MaPhieuNhap== maPN && x. MaVatTu== maVT)).SoLuongTon.Value;
            return sl;
        }

        public async Task<bool> CheckExistChiTiet(int maPKK)
        {
            var entity = await db.KiemKeChiTiets.FirstOrDefaultAsync(x=>x.MaPhieuKiemKe == maPKK);
            if (entity != null) return false;
            else return true;
        }
        public async Task<bool> SumSL(KiemKeChiTietViewModel model, int maPKK)
        {
            var entity = await db.KiemKeVatTus.FindAsync(maPKK);
            entity.TongTheoDoi += model.SoLuongTheoDoi;
            entity.TongThucTon += model.SoLuongThucTon;
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<KiemKeChiTietViewModel> GetById(int maPKK, int maPN, int maVT)
        {
            var entity = await db.KiemKeChiTiets.FirstOrDefaultAsync(x => x.MaPhieuKiemKe == maPKK && x.MaPhieuNhap == maPN && x.MaVatTu == maVT);
            var model = mp.Map<KiemKeChiTietViewModel>(entity);
            return model;
        }     
    }
}
