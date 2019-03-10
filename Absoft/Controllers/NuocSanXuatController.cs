using Absoft.Authorization;
using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Controllers
{
    public class NuocSanXuatController : BaseController
    {
        INuocSanXuatRepository _INuocSanXuatRepository;
        private readonly IAuthorizationService _authorizationService;

        public NuocSanXuatController(INuocSanXuatRepository INuocSanXuatRepository,
            IAuthorizationService authorizationService)
        {
            _INuocSanXuatRepository = INuocSanXuatRepository;
            _authorizationService = authorizationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var auth = await _authorizationService.AuthorizeAsync(User, "NUOC_SAN_XUAT", Operations.Read);
            //if (auth.Succeeded == false)
            //    return Unauthorized();

            var models = await _INuocSanXuatRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "NUOC_SAN_XUAT", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var paged = await _INuocSanXuatRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "NUOC_SAN_XUAT", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _INuocSanXuatRepository.GetByIdAsync(id);
            return Ok(models);
        }

        [HttpPut]
        public async Task<IActionResult> Update(NuocSanXuatViewModel nuocSanXuatViewModel)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "NUOC_SAN_XUAT", Operations.Update);
            if (auth.Succeeded == false)
                return Unauthorized();

            var result = await _INuocSanXuatRepository.UpdateAsync(nuocSanXuatViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(NuocSanXuatViewModel nuocSanXuatViewModel)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "NUOC_SAN_XUAT", Operations.Create);
            if (auth.Succeeded == false)
                return Unauthorized();

            var id = await _INuocSanXuatRepository.CheckTonTai(nuocSanXuatViewModel.TenNuoc);
            if (id == -1)
            {
                var result = await _INuocSanXuatRepository.InsertAsync(nuocSanXuatViewModel);
                return Ok(result);
            }
            else
            {
                return Ok(-1); // đã tồn tại không cho thêm
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "NUOC_SAN_XUAT", Operations.Delete);
            if (auth.Succeeded == false)
                return Unauthorized();

            var result = await _INuocSanXuatRepository.DeleteAsync(id);
            return Ok(result);
        }
        [HttpDelete("DeleteAllAsync/{strIds}")]
        public async Task<IActionResult> DeleteAllAsync(string strIds)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "NUOC_SAN_XUAT", Operations.Delete);
            if (auth.Succeeded == false)
                return Unauthorized();

            // strids [1,2,3,4,4,5,56,6]
            List<int> listId = JsonConvert.DeserializeObject<List<int>>(strIds);
            var result = await _INuocSanXuatRepository.DeleteAllAsync(listId);
            return Ok(result);
        }
    }
}