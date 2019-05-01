using Absoft.Helpers;
using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<RoleViewModel>> GetAllAsync();
        Task<PagedList<RoleViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<List<PhanQuyenViewModel>> GetListPermissionByRoleAsync(Guid roleId);
        Task<List<PhanQuyenViewModel>> GetListPermissionByRolesAsync(string[] roles);
        Task<List<PhanQuyenViewModel>> GetListPermissionByUserAsync(Guid? id);
        Task<RoleViewModel> GetByIdAsync(Guid? id);
        Task<RoleViewModel> GetByNameAsync(string name);
        Task<bool> AddAsync(RoleViewModel roleViewModel);
        Task<bool> UpdateAsync(RoleViewModel roleViewModel);
        Task<bool> DeleteAsync(Guid? id);
        Task<bool> EditRolesByUserAsync(string userName, RoleEditViewModel roleEditViewModel);
        Task<bool> CheckNameExistsAsync(string name);
        Task<bool> CheckPermissionAsync(string functionId, string action, string[] roles);
        Task<bool> SavePermission(Guid roleId, string functionId, string actionId);
    }
}
