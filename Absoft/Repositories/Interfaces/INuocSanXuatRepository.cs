using Absoft.Helpers;
using Absoft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface INuocSanXuatRepository
    {
        Task<List<NuocSanXuatViewModel>> GetAllAsync();
        Task<PagedList<NuocSanXuatViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<NuocSanXuatViewModel> GetByIdAsync(int id);
        Task<bool> InsertAsync(NuocSanXuatViewModel mnuocsanxuat);
        Task<bool> UpdateAsync(NuocSanXuatViewModel mnuocsanxuat);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsDelete(int id);
    }
}
