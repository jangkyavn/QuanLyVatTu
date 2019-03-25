using Absoft.Helpers;
using Absoft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IThongKeRepository
    {
        Task<IEnumerable<ThongKeNhapViewModel>> ThongKeNhap(ThongKeParams thongKeParams);
        Task<IEnumerable<ThongKeXuatViewModel>> ThongKeXuat(ThongKeParams thongKeParams);
        Task<IEnumerable<ThongKeThanhLyViewModel>> ThongKeThanhLy(ThongKeParams thongKeParams);
        Task<IEnumerable<ThongKeNhapXuatTonViewModel>> ThongKeNhapXuatTon(ThongKeParams thongKeParams);
    }
}
