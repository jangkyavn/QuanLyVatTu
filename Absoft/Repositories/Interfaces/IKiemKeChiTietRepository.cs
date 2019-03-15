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
        Task<bool> InserKiemKeChiTietAsync(KiemKeChiTietViewModel model, int maPKK, int maKho);
        Task<int> UpdateKiemKeChiTietAsync(KiemKeChiTietViewModel model, int maPKK, int maKho);
        Task<int> GetSoLuongTheoDoi(int maKho, int maPN, int maVT);
        Task<bool> CheckExistChiTiet(int maPKK);

        Task<bool> SumSL(KiemKeChiTietViewModel model, int maPKK);
        Task<KiemKeChiTietViewModel> GetById(int maPKK, int maPN, int maVT);
        
    }
}
