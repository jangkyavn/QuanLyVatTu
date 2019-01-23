using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    interface ILoaiVatTuRepository
    {
        Task<List<LoaiVatTuViewModel>> GetAllAsync();
        Task<string> GetNameByIdAsync(int id);
        Task<List<LoaiVatTuViewModel>> GetListLoaiByMaHM(int maHM);
        Task<bool> InsertAsync(LoaiVatTuViewModel loaivt, int maHM);
        Task<bool> UpdateAsync(LoaiVatTuViewModel loaivt, int maHM);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAllByMaHMAsync(int maHM);        
    }
}
