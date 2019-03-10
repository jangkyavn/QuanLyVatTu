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
    public class LoaiVatTuController : BaseController
    {
        ILoaiVatTuRepository _ILoaiVatTuRepository;
        private readonly IAuthorizationService _authorizationService;

        public LoaiVatTuController(ILoaiVatTuRepository ILoaiVatTuRepository,
            IAuthorizationService authorizationService)
        {
            _ILoaiVatTuRepository = ILoaiVatTuRepository;
            _authorizationService = authorizationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var auth = await _authorizationService.AuthorizeAsync(User, "LOAI_VAT_TU", Operations.Read);
            //if (auth.Succeeded == false)
            //    return Unauthorized();

            var models = await _ILoaiVatTuRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "LOAI_VAT_TU", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var paged = await _ILoaiVatTuRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailById(int id)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "LOAI_VAT_TU", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _ILoaiVatTuRepository.GetByIdAsync(id);
            return Ok(models);
        }
        [HttpGet("getListLoaiByMaHM/{maMH}")]
        public async Task<IActionResult> GetListLoaiByMaHM(int maMH)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "LOAI_VAT_TU", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _ILoaiVatTuRepository.GetListLoaiByMaHM(maMH);
            return Ok(models);
        }
        [HttpPut]
        public async Task<IActionResult> Update(LoaiVatTuViewModel LoaiVatTuViewModel)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "LOAI_VAT_TU", Operations.Update);
            if (auth.Succeeded == false)
                return Unauthorized();

            var result = await _ILoaiVatTuRepository.UpdateAsync(LoaiVatTuViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(LoaiVatTuViewModel LoaiVatTuViewModel)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "LOAI_VAT_TU", Operations.Create);
            if (auth.Succeeded == false)
                return Unauthorized();

            var id = await _ILoaiVatTuRepository.CheckTonTai(LoaiVatTuViewModel.TenLoai);
            if (id == -1)
            {
                var result = await _ILoaiVatTuRepository.InsertAsync(LoaiVatTuViewModel);
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
            var auth = await _authorizationService.AuthorizeAsync(User, "LOAI_VAT_TU", Operations.Delete);
            if (auth.Succeeded == false)
                return Unauthorized();

            var result = await _ILoaiVatTuRepository.DeleteAsync(id);
            return Ok(result);
        }
        [HttpDelete("DeleteAllAsync/{strIds}")]
        public async Task<IActionResult> DeleteAllAsync(string strIds)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "LOAI_VAT_TU", Operations.Delete);
            if (auth.Succeeded == false)
                return Unauthorized();

            // strids [1,2,3,4,4,5,56,6]
            List<int> listId = JsonConvert.DeserializeObject<List<int>>(strIds);
            var result = await _ILoaiVatTuRepository.DeleteAllAsync(listId);
            return Ok(result);
        }
    }
}