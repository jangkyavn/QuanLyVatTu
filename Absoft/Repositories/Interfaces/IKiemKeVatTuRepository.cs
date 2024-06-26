﻿using Absoft.Helpers;
using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IKiemKeVatTuRepository
    {
        Task<PagedList<KiemKeVatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<KiemKeVatTuViewModel> GetById(int maPKK);
        Task<int> InsertAsync(KiemKeVatTuViewModel model);
        Task<bool> DeleteAsync(int maPhieuKiemKe);
        Task<bool> UpdateAsync(KiemKeVatTuViewModel model);

        Task<bool> DisablePnPxPtl(string ngayKiemKe);
        Task<bool> UnablePnPxPtl(string ngayKiemKe);

        Task<PagedList<KhoHangViewModel>> GetListKho(PagingParams parginparam, int? maKho, int? maPN, int? maVT,int? maPKK,  bool status);
        Task<KiemKeVatTuParams> GetDetailAsync(int maPKK);
        //Task<List<int>> GetByMaKhoAsync(int maKho);
        //Task<List<VatTuViewModel>> GetListVatTuByMaKhoAsync(int maKho);
        //Task<ThanhLyVatTuParams> GetDetailAsync(int maPTL);
        //Task<List<VatTuViewModel>> GetListVTByMaPNAsync(int maPN);
        //Task<List<NhapChiTietViewModel>> GetListNhapChiTietByMaVTAsync(int maVT);
        //Task<PagedList<KhoHangViewModel>> GetListByMaKho(PagingParams pagingParams, int makho, string ngaythanhly);
        //Task<ThanhLyChiTietViewModel> GetThanhLyChiTiet(int maptl, int mapn, int mavt);
    }
}
