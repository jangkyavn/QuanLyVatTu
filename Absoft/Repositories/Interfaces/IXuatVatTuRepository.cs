using Absoft.Helpers;
using Absoft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IXuatVatTuRepository
    {
        Task<List<XuatVatTuViewModel>> GetAllAsync();
        Task<PagedList<XuatVatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<int> InsertAsync(XuatVatTuViewModel mxuatvt, List<XuatChiTietViewModel> listxuatchitiet);
        Task<bool> DeleteAsync(int maPX);
        Task<int> UpdateAsync(XuatVatTuViewModel mxuatvt, List<XuatChiTietViewModel> listxuatchitiet);        
        Task<XuatVatTuParams> GetDetailAsync(int maPX);
        Task<XuatVatTuParams> GetByMaPNAsync(int maPN);
        Task<List<int>> GetByMaKhoAsync(int maKho);        
        Task<List<VatTuViewModel>> GetListVTByMaPNAsync(int maPN);
        Task<List<VatTuViewModel>> GetListVatTuByMaKhoAsync(int maKho);
        Task<List<NhapChiTietViewModel>> GetListNhapChiTietByMaVTAsync(int maVT);
        Task<int> InsertXuatVatTu(XuatVatTuViewModel xuatVatTuViewModel);
        Task<PagedList<KhoHangViewModel>> GetListByMaKho(PagingParams pagingParams, int makho);
        Task<XuatChiTietViewModel> GetXuatChiTiet(int mapx, int mapn, int mavt);
        Task<bool> UpdateXuatVatTuAsync(XuatVatTuViewModel xuatVatTuViewModel);
        Task<bool> SumTongLuongTongTien(int id);
    }
}
