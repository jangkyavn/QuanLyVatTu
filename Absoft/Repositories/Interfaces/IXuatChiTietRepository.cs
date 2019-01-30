using Absoft.Helpers;
using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IXuatChiTietRepository
    {
        Task<bool> InsertAsync(XuatChiTietViewModel mxuatchitiet, int maphieuxuat);
        Task<int> UpdateXuatChiTietAsync(XuatChiTietViewModel mxuatchitiet, int maphieuxuat, int makho);
        Task<bool> DeleteXuatChiTietAsync(int maPX, int maPN, int maVT, int maKho);
        Task<bool> CheckDeleteXuatChiTietAsync(int maPX, int maPN, int maVT, int maKho);
        Task<bool> deleteAllXuatChiTiet(int maPX, int maPN, int maVT, int maKho);
        Task<CheckSoLuongParams> CheckSoLuongXuatChiTietAsync(int maphieunhap, int maphieuxuat, int makho, int mavt, int sl);
    }
}
