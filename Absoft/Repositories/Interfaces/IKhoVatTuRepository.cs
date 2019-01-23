using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    interface IKhoVatTuRepository
    {
        Task<List<KhoVatTuViewModel>> GetAllAsync();
        Task<string> GetNameByIdAsync(int id);
        Task<bool> InsertAsync(KhoVatTuViewModel mkhovattu);
        Task<bool> UpdateAsync(KhoVatTuViewModel mkhovattu);
        Task<bool> DeleteAsync(int id);
    }
}
