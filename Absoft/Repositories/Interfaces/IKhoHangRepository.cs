using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IKhoHangRepository
    {
        Task<List<KhoHangViewModel>> GetAllAsync();
        Task<List<KhoHangViewModel>> GetByMaKho(int id);
        Task<List<KhoHangViewModel>> GetByMaPhieuNhap(int id);
        Task<List<KhoHangViewModel>> GetByMaVatTu(int id);        
        Task<int> GetTongTon(int maVT, int maKho);
        Task<bool> UpdateAsync(KhoHangViewModel mkhohang);
        Task<bool> DeleteAsync(KhoHangViewModel mkhohang);
        Task<bool> InsertAsync(KhoHangViewModel mkhohang);
    }
}
