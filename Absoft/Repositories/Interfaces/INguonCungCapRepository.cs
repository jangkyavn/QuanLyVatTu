using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface INguonCungCapRepository
    {
        Task<List<NguonCungCapViewModel>> GetAllAsync();
        Task<NguonCungCapViewModel> GetByIdAsync(int id);
        Task<bool> InsertAsync(NguonCungCapViewModel mnguoncungcap);
        Task<bool> UpdateAsync(NguonCungCapViewModel mnguoncungcap);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsDelete(int id);
    }
}
