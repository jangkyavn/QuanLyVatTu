using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface INhapChiTietRepository
    {       
        Task<bool> InsertAsync(NhapChiTietViewModel mnhapchitiet, int maphieunhap);
        Task<int> UpdateNhapChiTietAsync(NhapChiTietViewModel mnhapchitiet, int maphieunhap, int makho);
        Task<bool> DeleteNhapChiTietAsync(int mapn, int mavt, int makho);
        Task<bool> CheckStatus(int mapn, int mavt, int makho);
        Task<bool> CheckDeleteNhapChiTietAsync(int mapn, int mavt, int makho);
    }
}
