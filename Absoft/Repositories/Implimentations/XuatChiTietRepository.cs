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
    public class XuatChiTietRepository: IXuatChiTietRepository
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
            kh.Status = true;
            db.KhoHangs.Update(kh);
            return await db.SaveChangesAsync()> 0;            
        }
        public async Task<bool> InsertAsync(XuatChiTietViewModel mxuatchitiet, int maphieuxuat)
        {
            var xct = mp.Map<XuatChiTiet>(mxuatchitiet);
            await db.XuatChiTiets.AddAsync(xct);
            return await db.SaveChangesAsync()>0;            
        }
        public async Task<int> UpdateXuatChiTietAsync(XuatChiTietViewModel mxuatchitiet, int maphieuxuat, int makho)
        {
            throw new NotImplementedException();
        }
    }
}
