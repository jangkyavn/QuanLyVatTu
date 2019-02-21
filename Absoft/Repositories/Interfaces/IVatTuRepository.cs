using Absoft.Helpers;
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
    }
}
