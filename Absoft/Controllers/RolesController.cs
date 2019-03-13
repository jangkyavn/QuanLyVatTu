using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Absoft.Controllers
{
    public class RolesController : BaseController
    {
        private readonly IRoleRepository _roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleRepository.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _roleRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("getListPermissionByRoleId/{roleId}")]
        public async Task<IActionResult> GetListPermissionByRoleId(Guid? roleId)
        {
            if (roleId == null)
            {
                return new BadRequestResult();
            }

            var model = await _roleRepository.GetByIdAsync(roleId);
            if (model == null)
            {
                return new NotFoundResult();
            }

            var models = await _roleRepository.GetListPermissionByRoleAsync(roleId.Value);
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            var model = await _roleRepository.GetByIdAsync(id.Value);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleViewModel roleViewModel)
        {
            var result = await _roleRepository.AddAsync(roleViewModel);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleViewModel roleViewModel)
        {
            var result = await _roleRepository.UpdateAsync(roleViewModel);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return new BadRequestObjectResult(ModelState);
            }

            var model = await _roleRepository.GetByIdAsync(id.Value);
            if (model == null)
            {
                return new NotFoundResult();
            }

            var result = await _roleRepository.DeleteAsync(id);
            return Ok(result);
        }

        [HttpGet("checkNameExists/{name}")]
        public async Task<IActionResult> ChecNameExists(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var result = await _roleRepository.CheckNameExistsAsync(name);
            return Ok(result);
        }

        [HttpGet("checkPermission/{functionId}/{actionType}")]
        public async Task<IActionResult> CheckPermission(string functionId, string actionType)
        {
            var rolesClaim = User.FindAll(ClaimTypes.Role);
            List<string> roles = new List<string>();
            foreach (var item in rolesClaim)
            {
                roles.Add(item.Value);
            }

            if (roles.Count > 0)
            {
                var result = await _roleRepository.CheckPermissionAsync(functionId, actionType, roles.ToArray());
                if (result || roles.Contains("Admin"))
                {
                    return Ok(true);
                }

                return Ok(false);
            }

            return Ok(false);
        }

        [HttpPut("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, RoleEditViewModel roleEditViewModel)
        {
            var result = await _roleRepository.EditRolesByUserAsync(userName, roleEditViewModel);

            return Ok(result);
        }

        [HttpPost("savePermission")]
        public async Task<IActionResult> SavePermission(PermissionParams permissionParams)
        {
            var result = await _roleRepository.SavePermission(permissionParams.RoleId, permissionParams.FunctionId, permissionParams.ActionId);
            return Ok(result);
        }
    }
}
