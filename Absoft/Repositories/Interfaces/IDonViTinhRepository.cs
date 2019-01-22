using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    interface IDonViTinhRepository
    {
        Task<List<DonViTinhViewModel>> GetAllAsync();
        Task<string> GetNameByIdAsync(int id);
        Task<bool> InsertAsync(DonViTinhViewModel mdonvitinh);
        Task<bool> UpdateAsync(DonViTinhViewModel mdonvitinh);
        Task<bool> DeleteAsync(int id);
    }
}
