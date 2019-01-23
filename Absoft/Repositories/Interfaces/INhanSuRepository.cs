using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface INhanSuRepository 
    {
        Task<List<NhanSuViewModel>> GetAllAsync();
        Task<NhanSuViewModel> GetByIdAsync(int id);
        Task<bool> InsertAsync(NhanSuViewModel mnhansu);
        Task<bool> UpdateAsync(NhanSuViewModel mnhansu);
        Task<bool> DeleteAsync(int id);
    }
}
