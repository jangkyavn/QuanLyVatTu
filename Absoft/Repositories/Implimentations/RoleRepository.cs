using Absoft.Data.Entities;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    }
}
