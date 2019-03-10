using Absoft.Helpers;
using Absoft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IChucNangRepository
    {
        Task<List<ChucNangViewModel>> GetAllAsync();
        Task<PagedList<ChucNangViewModel>> GetAllPagingAsync(PagingParams pagingParams);
    }
}
