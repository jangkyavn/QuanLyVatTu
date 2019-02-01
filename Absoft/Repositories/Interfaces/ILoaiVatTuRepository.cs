using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface ILoaiVatTuRepository
    {
        Task<List<LoaiVatTuViewModel>> GetAllAsync();
        Task<LoaiVatTuViewModel> GetByIdAsync(int id);
        Task<List<LoaiVatTuViewModel>> GetListLoaiByMaHM(int maHM);
        Task<bool> InsertAsync(LoaiVatTuViewModel loaivt);
        Task<bool> UpdateAsync(LoaiVatTuViewModel loaivt);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsDelete(int id);
        Task<bool> DeleteAllByMaHMAsync(int maHM);        
    }
}
