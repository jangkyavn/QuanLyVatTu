using Absoft.Helpers;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Http;
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
        Task<bool> GetStatus(int id);
        Task<bool> IsDeleteMulti(List<int> listid);
        Task<bool> InsertListAsync(List<DonViTinhViewModel> mdonvitinh);
        Task<bool> ImportDVT(IList<IFormFile> files);
        bool ExportDVT();
    }
}
