using Absoft.Helpers;
using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IThanhLyChiTietRepository
    {
        Task<bool> DeleteThanhLyChiTietAsync(int maPTL, int maPN, int maVT, int maKho);                
        Task<bool> InsertThanhLyChiTiet(ThanhLyChiTietViewModel thanhLyChiTietViewModel, int maPTL);
        Task<int> UpdateThanhLyChiTietAsync(ThanhLyChiTietViewModel thanhLyChiTietViewModel, int maPTL, int makho);
        Task<bool> CheckDeleteThanhLyChiTietAsync(int maPTL, int maPN, int maVT, int maKho);
        Task<bool> deleteAllThanhLyChiTiet(int maPTL, int maPN, int maVT, int maKho);
        Task<CheckSoLuongParams> CheckSoLuongThanhLyChiTietAsync(int maphieunhap, int mvt, int sl);
        Task<int> CheckTonTaiVTChitiet(int maptl, int maphieunhap, int mavt);
    }
}
