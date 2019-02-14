using Absoft.Helpers;
using Absoft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface ILoaiVatTuRepository
    {
        Task<List<LoaiVatTuViewModel>> GetAllAsync();
        Task<PagedList<LoaiVatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<LoaiVatTuViewModel> GetByIdAsync(int id);
        Task<List<LoaiVatTuViewModel>> GetListLoaiByMaHM(int maHM);
        Task<bool> InsertAsync(LoaiVatTuViewModel loaivt);
        Task<bool> UpdateAsync(LoaiVatTuViewModel loaivt);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsDelete(int id);
        Task<bool> DeleteAllByMaHMAsync(int maHM);
        Task<int> CheckTonTai(string name);
        Task<bool> ChangStatus(int id);
    }
}
