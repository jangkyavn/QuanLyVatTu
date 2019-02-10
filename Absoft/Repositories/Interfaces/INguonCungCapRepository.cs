using Absoft.Helpers;
using Absoft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface INguonCungCapRepository
    {
        Task<List<NguonCungCapViewModel>> GetAllAsync();
        Task<PagedList<NguonCungCapViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<NguonCungCapViewModel> GetByIdAsync(int id);
        Task<bool> InsertAsync(NguonCungCapViewModel mnguoncungcap);
        Task<bool> UpdateAsync(NguonCungCapViewModel mnguoncungcap);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsDelete(int id);
    }
}
