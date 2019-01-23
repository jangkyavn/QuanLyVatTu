using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    interface IVatTuRepository
    {
        Task<List<VatTuViewModel>> GetAllAsync();
        Task<string> GetNameByIdAsync(int id);
        Task<List<VatTuViewModel>> GetByMaLoaiVTAsync(int MaloaiVT);
        Task<List<VatTuViewModel>> GetNameByMaDVAsync(int MADV);
        Task<bool> InsertAsync(VatTuViewModel mvattu);
        Task<bool> UpdateAsync(VatTuViewModel mvattu);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteByMaLoaiVTAsync(int id);
    }
}
