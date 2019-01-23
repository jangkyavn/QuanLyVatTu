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
        Task<string> GetNameByIdAsync(int id);
        Task<bool> InsertAsync(HangMucVatTuViewModel mhangmucvattu);
        Task<bool> UpdateAsync(HangMucVatTuViewModel mhangmucvattu);
        Task<bool> DeleteAsync(int id);
    }
}
