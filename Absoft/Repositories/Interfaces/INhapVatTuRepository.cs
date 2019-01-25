using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface INhapVatTuRepository
    {
        Task<List<NhapVatTuViewModel>> GetAllAsync();        
        Task<bool> InsertAsync(NhapVatTuViewModel mnhapvattu, List<NhapChiTietViewModel> listnhapchitiet);        
        Task<bool> DeleteAsync(int id);
        Task<int> UpdateAsync(NhapVatTuViewModel mnhapvattu, List<NhapChiTietViewModel> listnhapchitiet);
        Task<bool> DeleteAsync( NhapVatTuViewModel mnhapvattu, List<NhapChiTietViewModel> listnhapchitiet);
    }
}
