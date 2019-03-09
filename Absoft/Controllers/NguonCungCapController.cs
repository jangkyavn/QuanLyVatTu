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

    public class NguonCungCapController : BaseController
    {
        INguonCungCapRepository _INguonCungCapRepository;
        private readonly IAuthorizationService _authorizationService;
        public NguonCungCapController(INguonCungCapRepository INguonCungCapRepository,
            IAuthorizationService authorizationService)
        {
            _INguonCungCapRepository = INguonCungCapRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "NGUON_CUNG_CAP", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _INguonCungCapRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "NGUON_CUNG_CAP", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var paged = await _INguonCungCapRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "NGUON_CUNG_CAP", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _INguonCungCapRepository.GetByIdAsync(id);
            return Ok(models);
        }

        [HttpPut]
        public async Task<IActionResult> Update(NguonCungCapViewModel nguonCungCapViewModel)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "NGUON_CUNG_CAP", Operations.Update);
            if (auth.Succeeded == false)
                return Unauthorized();

            var result = await _INguonCungCapRepository.UpdateAsync(nguonCungCapViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(NguonCungCapViewModel nguonCungCapViewModel)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "NGUON_CUNG_CAP", Operations.Create);
            if (auth.Succeeded == false)
                return Unauthorized();

            var id = await _INguonCungCapRepository.CheckTonTai(nguonCungCapViewModel.TenNguon);
            if (id == -1)
            {
                var result = await _INguonCungCapRepository.InsertAsync(nguonCungCapViewModel);
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
            var auth = await _authorizationService.AuthorizeAsync(User, "NGUON_CUNG_CAP", Operations.Delete);
            if (auth.Succeeded == false)
                return Unauthorized();

            var result = await _INguonCungCapRepository.DeleteAsync(id);
            return Ok(result);
        }
        [HttpDelete("DeleteAllAsync/{strIds}")]
        public async Task<IActionResult> DeleteAllAsync(string strIds)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "NGUON_CUNG_CAP", Operations.Delete);
            if (auth.Succeeded == false)
                return Unauthorized();

            // strids [1,2,3,4,4,5,56,6]
            List<int> listId = JsonConvert.DeserializeObject<List<int>>(strIds);
            var result = await _INguonCungCapRepository.DeleteAllAsync(listId);
            return Ok(result);
        }
    }
}