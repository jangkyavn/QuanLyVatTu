using Absoft.Helpers;
using Absoft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface INhapVatTuRepository
    {
        Task<List<NhapVatTuViewModel>> GetAllAsync();
        Task<PagedList<NhapVatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<bool> InsertAsync(NhapVatTuViewModel mnhapvattu, List<NhapChiTietViewModel> listnhapchitiet);        
        Task<bool> DeleteAsync(int maPN);
        Task<int> UpdateAsync(NhapVatTuViewModel mnhapvattu, List<NhapChiTietViewModel> listnhapchitiet);
        //Task<bool> DeleteAsync( NhapVatTuViewModel mnhapvattu, List<NhapChiTietViewModel> listnhapchitiet);
        Task<NhapVatTuParams> GetDetailAsync(int maPN);
        Task<int> InsertNhapVatTuAsync(NhapVatTuViewModel mnhapvattu);
        Task<bool> UpdateNhapVatTuAsync(NhapVatTuViewModel nhapVatTuViewModel);
        Task<bool> SumTongLuongTongTien(int id);
    }
}
