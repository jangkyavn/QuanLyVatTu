using Absoft.Helpers;
using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IXuatVatTuRepository
    {
        Task<List<XuatVatTuViewModel>> GetAllAsync();
        Task<int> InsertAsync(XuatVatTuViewModel mxuatvt, List<XuatChiTietViewModel> listxuatchitiet);
        Task<bool> DeleteAsync(int id);
        Task<int> UpdateAsync(XuatVatTuViewModel mxuatvt, List<XuatChiTietViewModel> listxuatchitiet);
        Task<bool> DeleteAsync(XuatVatTuViewModel mxuatvt, List<XuatChiTietViewModel> listxuatchitiet);
        Task<XuatVatTuParams> GetDetailAsync(int maPX);
        Task<XuatVatTuParams> GetByMaPNAsync(int maPN);
    }
}
