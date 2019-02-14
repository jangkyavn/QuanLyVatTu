using Absoft.Helpers;
using Absoft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IKhoHangRepository
    {
        Task<List<KhoHangViewModel>> GetAllAsync();
        Task<PagedList<KhoHangViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<List<KhoHangViewModel>> GetByMaKho(int id);
        Task<List<KhoHangViewModel>> GetByMaPhieuNhap(int id);
        Task<List<KhoHangViewModel>> GetByMaVatTu(int id);        
        Task<int> GetTongTon(int maVT, int maKho);
        Task<bool> UpdateAsync(KhoHangViewModel mkhohang);
        Task<bool> DeleteAsync(KhoHangViewModel mkhohang);
        Task<bool> InsertAsync(KhoHangViewModel mkhohang);
        Task<bool> TruSlXuat(KhoHangViewModel mkhohang);
        Task<int> GetSLTon(int maVT, int maKho, int maPN);
        Task<int> GetTotalCountAsync();

    }
}
