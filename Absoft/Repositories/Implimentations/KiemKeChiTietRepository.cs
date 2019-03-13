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
            // cong sl nguoc lai kho
            //var kh = await db.KhoHangs.FirstOrDefaultAsync(x => x.MaKho == maKho && x.MaPhieuNhap == maPN && x.MaVatTu == maVT);
            //kh.SoLuongTon += sltl;
            //try
            //{
            //    if (kh.SoLuongTon == (await db.NhapChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == maPN && x.MaVatTu == maVT)).SoLuong)
            //    {
            //        kh.Status = true;
            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
            //db.KhoHangs.Update(kh);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> InserKiemKeChiTietAsync(KiemKeChiTietViewModel model, int maPKK, int maKho)
        {
            var entity = mp.Map<KiemKeChiTiet>(model);
            // entity.MaPhieuKiemKe = maPKK;
            // entity.SoLuongTheoDoi =await GetSoLuongTheoDoi(maKho, model.MaPhieuNhap,model.MaVatTu);
            await db.KiemKeChiTiets.AddAsync(entity);
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateKiemKeChiTietAsync(KiemKeChiTietViewModel model, int maPKK, int maKho)
        {
            var kkct = await db.KiemKeChiTiets.FindAsync(maPKK);
            kkct.SoLuongThucTon = model.SoLuongThucTon;
            kkct.GhiChu = model.GhiChu;
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<int> GetSoLuongTheoDoi(int maKho, int maPN, int maVT)
        {
            var sl = (await db.KhoHangs.FirstOrDefaultAsync(x=>x.MaKho == maKho && x.MaPhieuNhap== maPN && x. MaVatTu== maVT)).SoLuongTon.Value;
            return sl;
        }
    }
}
