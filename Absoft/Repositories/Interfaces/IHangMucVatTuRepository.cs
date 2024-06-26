﻿using Absoft.Helpers;
using Absoft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IHangMucVatTuRepository
    {
        Task<List<HangMucVatTuViewModel>> GetAllAsync();
        Task<PagedList<HangMucVatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<HangMucVatTuViewModel> GetByIdAsync(int id);
        Task<bool> InsertAsync(HangMucVatTuViewModel mhangmucvattu);
        Task<bool> UpdateAsync(HangMucVatTuViewModel mhangmucvattu);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsDelete(int id);
        Task<int> CheckTonTai(string name);
        Task<bool> ChangStatus(int id);
        Task<bool> GetStatus(int id);
        Task<bool> IsDeleteMulti(List<int> listid);
        Task<bool> DeleteAllAsync(List<int> listId);
    }
}
