using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IThongKeRepository
    {
        Task<IEnumerable<ThongKeNhapViewModel>> ThongKeNhap(string fromDate, string toDate, int? maLoaiVT, int? maHM, int? maKho);
        Task<IEnumerable<ThongKeXuatViewModel>> ThongKeXuat(string fromDate, string toDate, int? maLoaiVT, int? maHM, int? maKho);
        Task<IEnumerable<ThongKeThanhLyViewModel>> ThongKeThanhLy(string fromDate, string toDate, int? maLoaiVT, int? maHM, int? maKho);
        Task<IEnumerable<ThongKeNhapXuatTonViewModel>> ThongKeNhapXuatTon(string fromDate, string toDate, int? maLoaiVT, int? maHM, int? maKho);
    }
}
