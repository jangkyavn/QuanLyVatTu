using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    interface INguonCungCapRepository
    {
        Task<List<NguonCungCapViewModel>> GetAllAsync();
        Task<string> GetNameByIdAsync(int id);
        Task<bool> InsertAsync(NguonCungCapViewModel mnguoncungcap);
        Task<bool> UpdateAsync(NguonCungCapViewModel mnguoncungcap);
        Task<bool> DeleteAsync(int id);
    }
}
