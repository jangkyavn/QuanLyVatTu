using Absoft.Authorization;
using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserManager<NguoiDung> _userManager;
        private readonly RoleManager<VaiTro> _roleManager;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public RoleRepository(
            UserManager<NguoiDung> userManager,
            RoleManager<VaiTro> roleManager,
            DataContext dataContext,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(RoleViewModel roleViewModel)
        {
            var model = _mapper.Map<VaiTro>(roleViewModel);
            var result = await _roleManager.CreateAsync(model);
            return result.Succeeded;
        }

        public async Task<bool> CheckNameExistsAsync(string name)
        {
            var model = await _roleManager.FindByNameAsync(name);
            return model != null;
        }

        public async Task<bool> CheckPermissionAsync(string functionId, string action, string[] roles)
        {
            var query = from cn in _dataContext.ChucNangs
                        join pq in _dataContext.PhanQuyens on cn.MaChucNang equals pq.MaChucNang
                        join vt in _roleManager.Roles on pq.MaVaiTro equals vt.Id
                        where roles.Contains(vt.Name) && cn.MaChucNang == functionId &&
                        ((pq.MaHanhDong == nameof(Operations.Read).ToUpper() && action == nameof(Operations.Read)) ||
                        (pq.MaHanhDong == nameof(Operations.Create).ToUpper() && action == nameof(Operations.Create)) ||
                        (pq.MaHanhDong == nameof(Operations.Update).ToUpper() == true && action == nameof(Operations.Update)) ||
                        (pq.MaHanhDong == nameof(Operations.Delete).ToUpper() == true && action == nameof(Operations.Delete)))
                        select pq;

            return await query.AnyAsync();
        }

        public async Task<bool> DeleteAsync(Guid? id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (_dataContext.PhanQuyens.Any(x => x.MaVaiTro == id))
            {
                var permissions = _dataContext.PhanQuyens.Where(x => x.MaVaiTro == id).ToList();
                if (permissions.Count > 0)
                {
                    _dataContext.PhanQuyens.RemoveRange(permissions);
                }
            }

            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public async Task<bool> EditRolesByUserAsync(string userName, RoleEditViewModel roleEditViewModel)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return false;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditViewModel.RoleNames;
            selectedRoles = selectedRoles ?? new string[] { };

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (result.Succeeded)
            {
                result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

                return result.Succeeded;
            }

            return false;
        }

        public async Task<List<RoleViewModel>> GetAllAsync()
        {
            return await _roleManager.Roles
                .OrderBy(x => x.Name)
                .ProjectTo<RoleViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PagedList<RoleViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from r in _roleManager.Roles
                        orderby r.Name
                        select new RoleViewModel
                        {
                            Id = r.Id,
                            Name = r.Name
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x =>
                    x.Name.ToUpper().Contains(keyword) ||
                    x.Name.ToUpper().ToUnSign().Contains(keyword.ToUnSign()));
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "name":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.Name);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.Name);
                        }
                        break;
                    default:
                        break;
                }
            }

            return await PagedList<RoleViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<RoleViewModel> GetByIdAsync(Guid? id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return _mapper.Map<RoleViewModel>(role);
        }

        public async Task<RoleViewModel> GetByNameAsync(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            return _mapper.Map<RoleViewModel>(role);
        }

        public async Task<List<PhanQuyenViewModel>> GetListPermissionByRoleAsync(Guid roleId)
        {
            var query = from cn in _dataContext.ChucNangs
                        join pq in _dataContext.PhanQuyens on cn.MaChucNang equals pq.MaChucNang
                        where pq.MaVaiTro == roleId
                        orderby cn.ViTri
                        select new PhanQuyenViewModel
                        {
                            MaVaiTro = roleId,
                            MaChucNang = cn.MaChucNang,
                            MaHanhDong = pq.MaHanhDong
                        };

            return await query.ToListAsync();
        }

        public async Task<List<PhanQuyenViewModel>> GetListPermissionByRolesAsync(string[] roles)
        {
            var query = from cn in _dataContext.ChucNangs
                        join pq in _dataContext.PhanQuyens on cn.MaChucNang equals pq.MaChucNang
                        join vt in _roleManager.Roles on pq.MaVaiTro equals vt.Id
                        where roles.Contains(vt.Name) && 
                        pq.MaHanhDong == nameof(Operations.Read).ToUpper() ||
                        pq.MaHanhDong == nameof(Operations.Create).ToUpper()
                        select pq;

            return await query.Distinct()
                .ProjectTo<PhanQuyenViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<PhanQuyenViewModel>> GetListPermissionByUserAsync(Guid? id)
        {
            var query = from cn in _dataContext.ChucNangs
                        join pq in _dataContext.PhanQuyens on cn.MaChucNang equals pq.MaChucNang
                        join ur in _dataContext.UserRoles on pq.MaVaiTro equals ur.RoleId
                        join nd in _dataContext.Users on ur.UserId equals nd.Id
                        where nd.Id == id
                        orderby cn.ViTri
                        select new PhanQuyenViewModel
                        {
                            MaVaiTro = pq.MaVaiTro,
                            MaChucNang = cn.MaChucNang,
                            MaHanhDong = pq.MaHanhDong
                        };

            return await query.ToListAsync();
        }

        public async Task<bool> SavePermission(Guid roleId, string functionId, string actionId)
        {
            var model = await _dataContext.PhanQuyens.FirstOrDefaultAsync(x => x.MaVaiTro == roleId && x.MaChucNang == functionId && x.MaHanhDong == actionId);

            if (model != null)
            {
                _dataContext.PhanQuyens.Remove(model);
            }
            else
            {
                var permission = new PhanQuyen()
                {
                    MaVaiTro = roleId,
                    MaChucNang = functionId,
                    MaHanhDong = actionId,
                };
                await _dataContext.PhanQuyens.AddAsync(permission);
            }

            var result = await _dataContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(RoleViewModel roleViewModel)
        {
            var role = await _roleManager.FindByIdAsync(roleViewModel.Id.ToString());
            role.Name = roleViewModel.Name;
            var result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
    }
}
