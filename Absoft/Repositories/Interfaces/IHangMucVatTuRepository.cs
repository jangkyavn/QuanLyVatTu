using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IHangMucVatTuRepository
    {
        Task<List<HangMucVatTuViewModel>> GetAllAsync();
        Task<HangMucVatTuViewModel> GetByIdAsync(int id);
        Task<bool> InsertAsync(HangMucVatTuViewModel mhangmucvattu);
        Task<bool> UpdateAsync(HangMucVatTuViewModel mhangmucvattu);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsDelete(int id);
    }
}
