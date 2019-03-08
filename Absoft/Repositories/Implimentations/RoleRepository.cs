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
        private readonly IMapper _mapper;

        public RoleRepository(
            UserManager<NguoiDung> userManager,
            RoleManager<VaiTro> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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

        public async Task<bool> DeleteAsync(Guid? id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
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

        public async Task<bool> UpdateAsync(RoleViewModel roleViewModel)
        {
            var role = await _roleManager.FindByIdAsync(roleViewModel.Id.ToString());
            role.Name = roleViewModel.Name;
            var result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
    }
}
