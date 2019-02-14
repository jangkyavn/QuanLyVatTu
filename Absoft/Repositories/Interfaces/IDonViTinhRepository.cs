using Absoft.Helpers;
using Absoft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IDonViTinhRepository
    {
        Task<List<DonViTinhViewModel>> GetAllAsync();
        Task<PagedList<DonViTinhViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<DonViTinhViewModel> GetById(int id);
        Task<bool> InsertAsync(DonViTinhViewModel mdonvitinh);
        Task<bool> UpdateAsync(DonViTinhViewModel mdonvitinh);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsDelete(int id);
        Task<int> CheckTonTai(string name);
        Task<bool> ChangStatus(int id);
    }
}
