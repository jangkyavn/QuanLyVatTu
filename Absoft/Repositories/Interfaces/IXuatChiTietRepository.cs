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
        Task<bool> DeleteXuatChiTietAsync(XuatChiTietViewModel mxuatchitiet, int maphieuxuat, int makho);
    }
}
