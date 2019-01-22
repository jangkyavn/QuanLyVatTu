using Absoft.Helpers;
using Absoft.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<RoleViewModel>> GetAllAsync();
        Task<bool> EditRolesByUserAsync(string userName, RoleEditViewModel roleEditViewModel);
    }
}
