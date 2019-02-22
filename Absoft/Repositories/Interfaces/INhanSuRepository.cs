using Absoft.Helpers;
using Absoft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface INhanSuRepository 
    {
        Task<List<NhanSuViewModel>> GetAllAsync();
        Task<PagedList<NhanSuViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<NhanSuViewModel> GetByIdAsync(int id);
        List<CityParam> LoadCities();
        List<DistrictsParam> LoadDistricts(int? cityId);
        List<NationsParam> LoadNations();
        Task<bool> InsertAsync(NhanSuViewModel mnhansu);
        Task<bool> UpdateAsync(NhanSuViewModel mnhansu);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsDelete(int id);
        Task<int> CheckTonTai(string name);
        Task<bool> ChangStatus(int id);
        Task<bool> GetStatus(int id);
        Task<bool> IsDeleteMulti(List<int> listid);
        Task<bool> DeleteAllAsync(List<int> listId);
    }
}
