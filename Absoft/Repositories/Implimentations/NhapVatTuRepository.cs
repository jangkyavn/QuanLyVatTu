using Absoft.Data;
using Absoft.Data.Entities;
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

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<NhapVatTuViewModel>> GetAllAsync()
        {
            return await db.NhapVatTus
                .ProjectTo<NhapVatTuViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
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
        public async Task<bool> UpdateAsync(NhapVatTuViewModel mnhapvattu, List<NhapChiTietViewModel> listnhapchitiet)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // tim ban ghi theo maphieu nhap
                    // sua cac truong tru tong tien, tong sl
                    // sua trong chi tiet
                    // sua trong kho
                    // cap nhap lai phieu nhap
                    transaction.Commit();
                    return await db.SaveChangesAsync() > 0;
                }
                catch (Exception)
                {
                    // TODO: Handle failure                    
                }
            }
            return false;
        }      
    }
}
