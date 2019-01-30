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
        public async Task<bool> DeleteAsync(int maPX)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {                    
                    var xvt = await db.XuatVatTus.FindAsync(maPX);                  
                    var listchitiet = db.XuatChiTiets.Where(x => x.MaPhieuXuat == maPX).ToList();
                    foreach (var item in listchitiet)
                    {
                        var check = await ixuatchitiet.DeleteXuatChiTietAsync(maPX, item.MaPhieuNhap,item.MaVatTu, xvt.MaKho);
                        if (check == false)
                        {
                            return false;
                        }
                    }
                    db.XuatVatTus.Remove(xvt);
                    transaction.Commit();
                    await db.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    // TODO: Handle failure                         
                }
            }
            return false;
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
            var chiTietVM = from ct in db.XuatChiTiets
                                  join vt in db.VatTus on ct.MaVatTu equals vt.MaVatTu
                                  where ct.MaPhieuXuat == maPX
                                  select new XuatChiTietViewModel
                                  {
                                      MaPhieuXuat = ct.MaPhieuXuat,
                                      MaPhieuNhap = ct.MaPhieuNhap,
                                      MaVatTu = ct.MaVatTu,
                                      TenVT = vt.TenVT,
                                      DonGia = ct.DonGia,
                                      SoLuongXuat = ct.SoLuongXuat,
                                      GhiChu = ct.GhiChu,
                                      Status = ct.Status
                                  };

            var mpx = mp.Map<XuatVatTuViewModel>(px);
            return new XuatVatTuParams()
            {
                mxuatvattu = mpx,
                listxuatchitiet = chiTietVM.ToList()
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
        public async Task<List<VatTuViewModel>> GetListVTByMaPNAsync(int maPN)
        {
            var vattu  =await (from nct in db.NhapChiTiets
                         join vt in db.VatTus on nct.MaVatTu equals vt.MaVatTu
                         where nct.MaPhieuNhap== maPN
                         select new VatTu
                         {
                                MaVatTu = nct.MaVatTu,
                                TenVT = vt.TenVT
                         }).ToListAsync();
            var mvattu = mp.Map<List<VatTuViewModel>>(vattu);
            return mvattu;
        }
        public async Task<List<int>> GetByMaKhoAsync(int maKho)
        {
            var listpn =await (from nvt in db.NhapVatTus
                       join kh in db.KhoHangs on nvt.MaKho equals kh.MaKho
                       where kh.MaKho == maKho
                       group nvt by nvt.MaPhieuNhap into g
                       select g.Key).ToListAsync();
            return listpn;
        }
        public async Task<int> InsertAsync(XuatVatTuViewModel mxuatvt, List<XuatChiTietViewModel> listxuatchitiet)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var nvt = await db.NhapVatTus.FirstOrDefaultAsync();
                    if(nvt.TongSoLuong<=0)
                    {
                        mxuatvt.TongSoLuong = 0;
                        mxuatvt.TongSoTien = 0;
                    }                 
                    // tao phieu xuat de lay mapx
                    var px = mp.Map<XuatVatTu>(mxuatvt);
                    await db.XuatVatTus.AddAsync(px);
                    var res1 = await db.SaveChangesAsync();
                    if (res1 > 0)
                    {
                        foreach (var item in listxuatchitiet)
                        {
                            // check sl >= item.soluong
                            var slkho = await ikhohang.GetSLTon(item.MaVatTu, px.MaKho, item.MaPhieuNhap);
                            if (slkho != 0 && slkho >= item.SoLuongXuat)
                            {
                                var res = await ixuatchitiet.InsertAsync(item, px.MaPhieuXuat);
                                if (res == true)
                                {
                                    mxuatvt.TongSoTien += item.DonGia * item.SoLuongXuat;
                                    mxuatvt.TongSoLuong += item.SoLuongXuat;
                                    // tru so luong trong kho = 0 thi xoa trong kho
                                    // set status = fales khong cho xoa nhapchitiet cua mat hang nay vi da xuat
                                    var mkh = new KhoHangViewModel()
                                    {
                                        SoLuongTon = slkho - item.SoLuongXuat,
                                        MaKho = px.MaKho,
                                        MaPhieuNhap = item.MaPhieuNhap,
                                        MaVatTu = item.MaVatTu,
                                        Status = false
                                    };
                                    var kh = mp.Map<KhoHang>(mkh);
                                    //if ((slkho - item.SoLuongXuat) == 0) db.KhoHangs.Remove(kh);
                                    db.KhoHangs.Update(kh);
                                }
                                else return 0;
                            }
                            else return -1; // so luog xuat vuot qua sl ton                        
                        }
                    }
                    else return 0;
                    // insert phieu xuat chi tiet
                    px.TongSoLuong = mxuatvt.TongSoLuong;
                    px.TongSoTien = mxuatvt.TongSoTien;
                    db.XuatVatTus.Update(px);
                    transaction.Commit();
                    await db.SaveChangesAsync();
                    return 1; // thành công
                }
                catch (Exception)
                {
                    // TODO: Handle failure                    
                }
            }
            return 0;// có lỗi
        }
        public async Task<int> UpdateAsync(XuatVatTuViewModel mxuatvt, List<XuatChiTietViewModel> listxuatchitiet)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //mxuatvt.TongSoLuong = 0;
                    //mxuatvt.TongSoTien = 0;                    
                    // tim ban ghi theo maphieu xuat
                    var xvt = mp.Map<XuatVatTu>(mxuatvt);
                    // sua cac truong tru tong tien, tong sl
                    db.XuatVatTus.Update(xvt);
                    // sua trong chi tiet
                    foreach (var item in listxuatchitiet)
                    {
                        // lấy sl cũ và trừ trong tổng
                        var ctcu = db.XuatChiTiets.FirstOrDefault(x => x.MaPhieuNhap == item.MaPhieuNhap && x.MaPhieuXuat == item.MaPhieuXuat && x.MaVatTu == item.MaVatTu);
                        mxuatvt.TongSoLuong -= ctcu.SoLuongXuat;
                        mxuatvt.TongSoTien -= ctcu.DonGia * ctcu.SoLuongXuat;
                        // thêm sl mới vào tổng
                        mxuatvt.TongSoTien += item.DonGia * item.SoLuongXuat;
                        mxuatvt.TongSoLuong += item.SoLuongXuat;
                        int sltonmoi = await ixuatchitiet.UpdateXuatChiTietAsync(item, mxuatvt.MaPhieuXuat, mxuatvt.MaKho);
                        if (sltonmoi >= 0)
                        {
                            // update vào kho hàng so luong ton moi
                            var khohang = db.KhoHangs.FirstOrDefault(x => x.MaKho == mxuatvt.MaKho && x.MaPhieuNhap == item.MaPhieuNhap && x.MaVatTu == item.MaVatTu);
                            khohang.SoLuongTon = sltonmoi;
                            //if (sltonmoi == 0) db.KhoHangs.Remove(khohang);
                            if (sltonmoi == (await db.NhapChiTiets.FirstOrDefaultAsync(x => x.MaPhieuNhap == item.MaPhieuNhap && x.MaVatTu == item.MaVatTu)).SoLuong)
                            {
                                khohang.Status = true;
                            }
                            else
                            {
                                khohang.Status = false;
                                db.KhoHangs.Update(khohang);
                            }
                        }
                        else return -1;
                    }
                    // sua trong kho
                    // cap nhap lai phieu nhap
                    xvt.TongSoLuong = mxuatvt.TongSoLuong;
                    xvt.TongSoTien = mxuatvt.TongSoTien;                    
                    db.XuatVatTus.Update(xvt);
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
        public async Task<List<VatTuViewModel>> GetListVatTuByMaKhoAsync(int maKho)
        {
            var query = from kh in db.KhoHangs
                        where kh.MaKho == maKho
                        select kh.MaVatTu;

            var listpn = await (from q in query
                                join vt in db.VatTus on q equals vt.MaVatTu
                                select new VatTuViewModel
                                {
                                    MaVatTu = vt.MaVatTu,
                                    TenVT = vt.TenVT
                                }).ToListAsync();
            return listpn;
        }
        public async Task<List<NhapChiTietViewModel>> GetListNhapChiTietByMaVTAsync(int maVT)
        {
            var listnct = await db.NhapChiTiets.Where(x => x.MaVatTu == maVT).ToListAsync();
            return mp.Map<List<NhapChiTietViewModel>>(listnct);
        }
    }
}
