using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    interface INuocSanXuatRepository
    {
        Task<List<NuocSanXuatViewModel>> GetAllAsync();
        Task<string> GetNameByIdAsync(int id);
        Task<bool> InsertAsync(NuocSanXuatViewModel mnuocsanxuat);
        Task<bool> UpdateAsync(NuocSanXuatViewModel mnuocsanxuat);
        Task<bool> DeleteAsync(int id);
    }
}
