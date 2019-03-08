using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IKiemKeChiTietRepository
    {
        Task<bool> DeleteKiemKeChiTietAsync(int maPKK, int maPN, int maVT, int maKho);
        Task<bool> InserKiemKeChiTietAsync(KiemKeChiTietViewModel model, int maPKK);
        Task<int> UpdateKiemKeChiTietAsync(KiemKeChiTietViewModel model, int maPKK, int maKho);
    }
}
