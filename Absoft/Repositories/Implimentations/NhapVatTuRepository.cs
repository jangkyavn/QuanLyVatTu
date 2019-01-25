using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class NhapVatTuRepository : INhapVatTuRepository
    {
        DataContext db;
        IMapper mp;
        INhapChiTietRepository inhapchitiet;
        IKhoHangRepository ikhohang;
        public NhapVatTuRepository(DataContext data, IMapper mapper, INhapChiTietRepository INhapChiTietRepository, IKhoHangRepository IKhoHangRepository)
        {
            db = data;
            mp = mapper;
            inhapchitiet = INhapChiTietRepository;
            ikhohang = IKhoHangRepository;
        }

        public async Task<bool> DeleteAsync(int maPN)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // tim ban ghi theo maphieu nhap
                    var nvt = await db.NhapVatTus.FindAsync(maPN);
                    // sua cac truong tru tong tien, tong sl                  
                    // sua trong chi tiet
                    var listnhapchitiet = db.NhapChiTiets.Where(x => x.MaPhieuNhap == maPN).ToList();                    
                    foreach (var item in listnhapchitiet)
                    {
                        var check = await inhapchitiet.DeleteNhapChiTietAsync(maPN, item.MaVatTu, nvt.MaKho);
                        if (check == false)
                        {
                            return false; // loi vi chi tiet da xuat, tra ve chi tiet
                        }
                    }
                    db.NhapVatTus.Remove(nvt);
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
        public async Task<List<NhapVatTuViewModel>> GetAllAsync()
        {
            var model = from nvt in db.NhapVatTus
                        join hm in db.HangMucVatTus on nvt.MaHM equals hm.MaHM
                        join kvt in db.KhoVatTus on nvt.MaKho equals kvt.MaKho
                        select new NhapVatTuViewModel
                        {
                            MaPhieuNhap= nvt.MaPhieuNhap,
                            MaHM = nvt.MaHM,
                            MaKho = nvt.MaKho,
                            TenHM= hm.TenHM,
                            TenKho = kvt.TenKho,
                            NgayNhap = nvt.NgayNhap,
                            NguoiNhap = nvt.NguoiNhap,
                            TongSoTien= nvt.TongSoTien,
                            TongSoLuong= nvt.TongSoLuong,
                            ChietKhau = nvt.ChietKhau,
                            GhiChu = nvt.GhiChu,
                            Status= nvt.Status
                        };
            return await model.ToListAsync();
        }
        public async Task<NhapVatTuParams> GetDetailAsync(int maPN)
        {
            var pn = await db.NhapVatTus.FindAsync(maPN);
            var listpnct = db.NhapChiTiets.Where(x => x.MaPhieuNhap == maPN).ToList();
            var mpn = mp.Map<NhapVatTuViewModel>(pn);
            var mlistpnct = mp.Map<List<NhapChiTietViewModel>>(listpnct);
            return new NhapVatTuParams()
            {
                mnhapvattu = mpn,
                listnhapchitiet = mlistpnct
            };
        }
        public async Task<bool> InsertAsync(NhapVatTuViewModel mnhapvattu,List<NhapChiTietViewModel>  listnhapchitiet)
        {           
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    mnhapvattu.TongSoLuong = 0;
                    mnhapvattu.TongSoTien = 0;
                    var pn = mp.Map<NhapVatTu>(mnhapvattu);
                    await db.NhapVatTus.AddAsync(pn);                    
                    var rs =  await db.SaveChangesAsync();
                    if (rs > 0)
                    {
                        // gọi hàm nhập vào phiếu nhập chi tiết
                        foreach (var item in listnhapchitiet)
                        {
                            var result = await inhapchitiet.InsertAsync(item, pn.MaPhieuNhap);
                            if (result == true)
                            {
                                mnhapvattu.TongSoTien += item.DonGia * item.SoLuong;
                                mnhapvattu.TongSoLuong += item.SoLuong;
                                // insert vào kho hàng theo chi tiết
                                var khohang = new KhoHangViewModel()
                                {
                                    MaKho = pn.MaKho,
                                    MaPhieuNhap = pn.MaPhieuNhap,
                                    MaVatTu = item.MaVatTu,
                                    SoLuongTon = item.SoLuong
                                };
                                // gọi hàm nhập vào kho
                                await ikhohang.InsertAsync(khohang);
                            }
                            else return false;
                        }
                        // cập nhật tổng sl, tổng tiền vào phiếu nhập 
                        pn.TongSoLuong = mnhapvattu.TongSoLuong;
                        pn.TongSoTien = mnhapvattu.TongSoTien;
                        db.NhapVatTus.Update(pn);
                        transaction.Commit();
                        return await db.SaveChangesAsync() > 0;
                    }
                    else return false;                                                                        
                }
                catch (Exception)
                {
                    // TODO: Handle failure                    
                }
            }
            return false;
        }
        public async Task<int> UpdateAsync(NhapVatTuViewModel mnhapvattu, List<NhapChiTietViewModel> listnhapchitiet)
        {

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    mnhapvattu.TongSoLuong = 0;
                    mnhapvattu.TongSoTien = 0;
                    // tim ban ghi theo maphieu nhap
                    var nvt = mp.Map<NhapVatTu>(mnhapvattu);
                    // sua cac truong tru tong tien, tong sl
                    db.NhapVatTus.Update(nvt);
                    // sua trong chi tiet
                    foreach (var item in listnhapchitiet)
                    {
                        mnhapvattu.TongSoTien += item.DonGia * item.SoLuong;
                        mnhapvattu.TongSoLuong += item.SoLuong;
                        int sltonmoi = await inhapchitiet.UpdateNhapChiTietAsync(item, mnhapvattu.MaPhieuNhap.Value,mnhapvattu.MaKho);
                        if (sltonmoi >=0)
                        {
                            // update vào kho hàng so luong ton moi
                            var khohang = db.KhoHangs.Where(x=>x.MaKho ==mnhapvattu.MaKho && x.MaPhieuNhap == mnhapvattu.MaPhieuNhap && x.MaVatTu==item.MaVatTu).FirstOrDefault();
                            khohang.SoLuongTon = sltonmoi;
                            db.KhoHangs.Update(khohang);
                        }
                        else return -1;
                    }
                    // sua trong kho
                    // cap nhap lai phieu nhap
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
    }
}
