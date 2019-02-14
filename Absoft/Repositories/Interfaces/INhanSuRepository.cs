using Absoft.Helpers;
using Absoft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface INhanSuRepository 
    {
        Task<List<NhanSuViewModel>> GetAllAsync();
        Task<PagedList<NhanSuViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<NhanSuViewModel> GetByIdAsync(int id);
        Task<bool> InsertAsync(NhanSuViewModel mnhansu);
        Task<bool> UpdateAsync(NhanSuViewModel mnhansu);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsDelete(int id);
        Task<int> CheckTonTai(string name);
        Task<bool> ChangStatus(int id);
    }
}
