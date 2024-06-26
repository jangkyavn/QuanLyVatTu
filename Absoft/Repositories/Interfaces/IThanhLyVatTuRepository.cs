﻿using Absoft.Helpers;
using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IThanhLyVatTuRepository
    {        
        Task<PagedList<ThanhLyVatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<int> InserThanhLyVatTu(ThanhLyVatTuViewModel thanhLyVatTuViewModel);
        Task<bool> DeleteAsync(int maPhieuThanhLy);
        Task<bool> UpdateThanhLyVatTuAsync(ThanhLyVatTuViewModel thanhLyVatTuViewModel);

        Task<List<int>> GetByMaKhoAsync(int maKho);
        Task<List<VatTuViewModel>> GetListVatTuByMaKhoAsync(int maKho);
        Task<ThanhLyVatTuParams> GetDetailAsync(int maPTL);
        Task<List<VatTuViewModel>> GetListVTByMaPNAsync(int maPN);
        Task<List<NhapChiTietViewModel>> GetListNhapChiTietByMaVTAsync(int maVT);
        Task<PagedList<KhoHangViewModel>> GetListByMaKho(PagingParams pagingParams, int makho, string ngaythanhly);
        Task<ThanhLyChiTietViewModel> GetThanhLyChiTiet(int maptl, int mapn, int mavt);
    }
}
