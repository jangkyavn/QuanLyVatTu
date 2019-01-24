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
        Task<NhapVatTuViewModel> GetById(int id);
        Task<bool> InsertAsync(NhapVatTuViewModel mdonvitinh);
        Task<bool> UpdateAsync(NhapVatTuViewModel mdonvitinh);
        Task<bool> DeleteAsync(int id);
    }
}
