using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    interface IHangSanXuatRepository
    {
        Task<List<HangSanXuatViewModel>> GetAllAsync();
        Task<string> GetNameByIdAsync(int id);
        Task<bool> InsertAsync(HangSanXuatViewModel mhangsanxuat);
        Task<bool> UpdateAsync(HangSanXuatViewModel mhangsanxuat);
        Task<bool> DeleteAsync(int id);
    }
}
