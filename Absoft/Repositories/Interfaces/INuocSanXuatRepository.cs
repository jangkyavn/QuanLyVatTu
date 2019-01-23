using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface INuocSanXuatRepository
    {
        Task<List<NuocSanXuatViewModel>> GetAllAsync();
        Task<NuocSanXuatViewModel> GetByIdAsync(int id);
        Task<bool> InsertAsync(NuocSanXuatViewModel mnuocsanxuat);
        Task<bool> UpdateAsync(NuocSanXuatViewModel mnuocsanxuat);
        Task<bool> DeleteAsync(int id);
    }
}
