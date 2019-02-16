using Absoft.Helpers;
using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IKhoVatTuRepository
    {
        Task<List<KhoVatTuViewModel>> GetAllAsync();
        Task<PagedList<KhoVatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<KhoVatTuViewModel> GetByIdAsync(int id);
        Task<bool> InsertAsync(KhoVatTuViewModel mkhovattu);
        Task<bool> UpdateAsync(KhoVatTuViewModel mkhovattu);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsDelete(int id);
        Task<int> CheckTonTai(string name);
        Task<bool> ChangStatus(int id);
        Task<bool> GetStatus(int id);
        Task<bool> IsDeleteMulti(List<int> listid);
    }
}
