using Absoft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IHanhDongRepository
    {
        Task<List<HanhDongViewModel>> GetAllAsync();
    }
}
