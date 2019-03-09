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
    public class DonViTinhController : BaseController
    {
        IDonViTinhRepository _donViTinhRepository;
        private readonly IAuthorizationService _authorizationService;

        public List<int> JsonConnvert { get; private set; }

        public DonViTinhController(
            IDonViTinhRepository donViTinhRepository,
            IAuthorizationService authorizationService)
        {
            _donViTinhRepository = donViTinhRepository;
            _authorizationService = authorizationService;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "DON_VI_TINH", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _donViTinhRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "DON_VI_TINH", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var paged = await _donViTinhRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "DON_VI_TINH", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _donViTinhRepository.GetById(id);
            return Ok(models);
        }
        [HttpPut]
        public async Task<IActionResult> Update(DonViTinhViewModel donViTinhViewModel)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "DON_VI_TINH", Operations.Update);
            if (auth.Succeeded == false)
                return Unauthorized();

            var result = await _donViTinhRepository.UpdateAsync(donViTinhViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(DonViTinhViewModel donViTinhViewModel)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "DON_VI_TINH", Operations.Create);
            if (auth.Succeeded == false)
                return Unauthorized();

            var id = await _donViTinhRepository.CheckTonTai(donViTinhViewModel.TenDVT);
            if (id == -1)
            {
                var result = await _donViTinhRepository.InsertAsync(donViTinhViewModel);
                return Ok(result);
            }
            else
            {
                return Ok(-1);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "DON_VI_TINH", Operations.Delete);
            if (auth.Succeeded == false)
                return Unauthorized();

            var result = await _donViTinhRepository.DeleteAsync(id);
            return Ok(result);
        }
        [HttpDelete("DeleteAllAsync/{strIds}")]
        public async Task<IActionResult> DeleteAllAsync(string strIds)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "DON_VI_TINH", Operations.Delete);
            if (auth.Succeeded == false)
                return Unauthorized();

            // strids [1,2,3,4,4,5,56,6]
            List<int> listId = JsonConvert.DeserializeObject<List<int>>(strIds);
            var result = await _donViTinhRepository.DeleteAllAsync(listId);
            return Ok(result);
        }
    }
}
