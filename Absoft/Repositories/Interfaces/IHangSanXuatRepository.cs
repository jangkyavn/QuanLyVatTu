using Absoft.Helpers;
using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IHangSanXuatRepository
    {
        Task<List<HangSanXuatViewModel>> GetAllAsync();
        Task<PagedList<HangSanXuatViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<HangSanXuatViewModel> GetByIdAsync(int id);
        Task<bool> InsertAsync(HangSanXuatViewModel mhangsanxuat);
        Task<bool> UpdateAsync(HangSanXuatViewModel mhangsanxuat);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsDelete(int id);
    }
}
