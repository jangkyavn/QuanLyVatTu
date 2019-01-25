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
    public class XuatVatTuRepository : IXuatVatTuRepository
    {
        DataContext db;
        IMapper mp;      
        IKhoHangRepository ikhohang;
        IXuatChiTietRepository ixuatchitiet;
        public XuatVatTuRepository(DataContext data, IMapper mapper, IKhoHangRepository IKhoHangRepository, IXuatChiTietRepository IXuatChiTietRepository)
        {
            db = data;
            mp = mapper;
            ikhohang = IKhoHangRepository;
            ixuatchitiet = IXuatChiTietRepository;
        }       
        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(XuatVatTuViewModel mxuatvt, List<XuatChiTietViewModel> listxuatchitiet)
        {
            throw new NotImplementedException();
        }

        public async Task<List<XuatVatTuViewModel>> GetAllAsync()
        {
            var model = from xvt in db.XuatVatTus
                        join kvt in db.KhoVatTus on xvt.MaKho equals kvt.MaKho
                        join ns in db.NhanSus on xvt.MaNS equals ns.MaNS
                        select new XuatVatTuViewModel
                        {
                            MaPhieuXuat = xvt.MaPhieuXuat,
                            MaKho = xvt.MaKho,
                            MaNS = xvt.MaNS,
                            NgayNhap = xvt.NgayNhap,
                            TongSoTien = xvt.TongSoTien,
                            TongSoLuong = xvt.TongSoLuong,
                            GhiChu = xvt.GhiChu,
                            Status = xvt.Status,
                            TenKho = kvt.TenKho,
                            TenNS = ns.HoTen
                        };
            return await model.ToListAsync();
        }
        public async Task<XuatVatTuParams> GetDetailAsync(int maPX)
        {
            var px = await db.XuatVatTus.FindAsync(maPX);
            var listpxct = db.XuatChiTiets.Where(x => x.MaPhieuXuat == maPX).ToList();
            var mpx = mp.Map<XuatVatTuViewModel>(px);
            var mlistpxct = mp.Map<List<XuatChiTietViewModel>>(listpxct);
            return new XuatVatTuParams()
            {
                mxuatvattu = mpx,
                listxuatchitiet = mlistpxct
            };
        }
        public async Task<XuatVatTuParams> GetByMaPNAsync(int maPN)
        {
            var xvts = from nvt in db.NhapVatTus
                      where nvt.MaPhieuNhap == maPN
                      select new XuatVatTuViewModel
                      {
                          MaKho= nvt.MaKho,
                          NgayNhap=nvt.NgayNhap,
                          TongSoLuong=nvt.TongSoLuong,
                          TongSoTien= nvt.TongSoTien,
                          GhiChu = nvt.GhiChu
                      };
            var xvt = await xvts.FirstOrDefaultAsync();
            var xcts = from nct in db.NhapChiTiets
                      join kh in db.KhoHangs on nct.MaPhieuNhap equals kh.MaPhieuNhap 
                      where nct.MaPhieuNhap == maPN && kh.MaVatTu == nct.MaVatTu
                      select new XuatChiTietViewModel
                      {
                          MaPhieuNhap = nct.MaPhieuNhap,
                         MaVatTu = nct.MaVatTu,
                         SoLuongXuat = kh.SoLuongTon,
                         DonGia = nct.DonGia,
                         GhiChu = nct.GhiChu
                      };           
            var mxvt = mp.Map<XuatVatTuViewModel>(xvt);
            var mlistxuatct = mp.Map<List<XuatChiTietViewModel>>(xcts.ToList());
            return new XuatVatTuParams()
            {
                mxuatvattu = mxvt,
                listxuatchitiet = mlistxuatct
            };
        }
        public async Task<int> InsertAsync(XuatVatTuViewModel mxuatvt, List<XuatChiTietViewModel> listxuatchitiet)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // tao phieu xuat de lay mapx
                    var px = mp.Map<XuatVatTu>(mxuatvt);
                    await db.XuatVatTus.AddAsync(px);
                    // insert phieu xuat chi tiet
                    foreach(var item in listxuatchitiet)
                    {
                        var res = ixuatchitiet.InsertAsync(item,px.MaPhieuXuat);
                        // tru so luong trong kho = 0 thi xoa trong kho
                    }
                    transaction.Commit();
                    await db.SaveChangesAsync();
                    return 1;
                }
                catch (Exception)
                {
                    // TODO: Handle failure                    
                }
            }
            return 0;
        }
        public async Task<int> UpdateAsync(XuatVatTuViewModel mxuatvt, List<XuatChiTietViewModel> listxuatchitiet)
        {
            throw new NotImplementedException();
        }
    }
}
