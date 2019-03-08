using Absoft.Data;
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

        public Task<bool> InserKiemKeChiTietAsync(KiemKeChiTietViewModel model, int maPKK)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateKiemKeChiTietAsync(KiemKeChiTietViewModel model, int maPKK, int maKho)
        {
            throw new NotImplementedException();
        }
    }
}
