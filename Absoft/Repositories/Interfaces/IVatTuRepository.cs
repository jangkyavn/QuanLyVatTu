﻿using Absoft.Helpers;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IVatTuRepository
    {
        Task<List<VatTuViewModel>> GetAllAsync();
        Task<PagedList<VatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<PagedList<VatTuViewModel>> GetAllPagingWithTongTonAsync(PagingParams pagingParams);
        Task<VatTuViewModel> GetByIdAsync(int id);
        Task<List<VatTuViewModel>> GetByMaLoaiVTAsync(int MaloaiVT);
        Task<List<VatTuViewModel>> GetByMaDVAsync(int MADV);
        Task<bool> InsertAsync(VatTuViewModel mvattu);
        Task<bool> UpdateAsync(VatTuViewModel mvattu);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsDelete(int id);
        Task<bool> DeleteByMaLoaiVTAsync(int id);
        Task<int> GetTotalCountAsync();
        Task<List<VatTuViewModel>> GetByMaHM(int maHM);
        Task<int> CheckTonTai(string name);
        Task<bool> ChangStatus(int id);
        Task<bool> GetStatus(int id);
        Task<bool> IsDeleteMulti(List<int> listid);
        Task<bool> ImportVT(IList<IFormFile> files);
        object ExportVT(List<VatTuViewModel> vatTuViewModels);
        bool DeleteFileVTAfterExport(string filename);
        Task<bool> DeleteAllAsync(List<int> listId);

        Task<PagedListParam> ThongKeVatTuNhapByMaVT(PagingParams pagingParams, int mavt);
        Task<PagedListParam> ThongKeVatTuXuatpByMaVT(PagingParams pagingParams, int mavt);
        Task<PagedListParam> ThongKeVatTuThanhLyByMaVT(PagingParams pagingParams, int mavt);
        Task<PagedListParam> ThongKeVatTuTonKhoByMaVT(PagingParams pagingParams, int mavt);
    }
}
