using Absoft.Authorization;
using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Absoft.Controllers
{
    public class HangMucVatTuController : BaseController
    {
        IHangMucVatTuRepository _hangMucVatTuRepository;
        private readonly IAuthorizationService _authorizationService;

        public HangMucVatTuController(IHangMucVatTuRepository hangMucVatTuRepository,
            IAuthorizationService authorizationService)
        {
            _hangMucVatTuRepository = hangMucVatTuRepository;
            _authorizationService = authorizationService;
        }       

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var auth = await _authorizationService.AuthorizeAsync(User, "HANG_MUC_VAT_TU", Operations.Read);
            //if (auth.Succeeded == false)
            //    return Unauthorized();

            var models = await _hangMucVatTuRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "HANG_MUC_VAT_TU", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var paged = await _hangMucVatTuRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "HANG_MUC_VAT_TU", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _hangMucVatTuRepository.GetByIdAsync(id);
            return Ok(models);
        }

        [HttpPut]
        public async Task<IActionResult> Update(HangMucVatTuViewModel hangMucVatTuViewModel)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "HANG_MUC_VAT_TU", Operations.Update);
            if (auth.Succeeded == false)
                return Unauthorized();

            var result = await _hangMucVatTuRepository.UpdateAsync(hangMucVatTuViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(HangMucVatTuViewModel hangMucVatTuViewModel)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "HANG_MUC_VAT_TU", Operations.Create);
            if (auth.Succeeded == false)
                return Unauthorized();

            var id = await _hangMucVatTuRepository.CheckTonTai(hangMucVatTuViewModel.TenHM);
            if (id == -1)
            {
                var result = await _hangMucVatTuRepository.InsertAsync(hangMucVatTuViewModel);
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
            var auth = await _authorizationService.AuthorizeAsync(User, "HANG_MUC_VAT_TU", Operations.Delete);
            if (auth.Succeeded == false)
                return Unauthorized();

            var result = await _hangMucVatTuRepository.DeleteAsync(id);
            return Ok(result);
        }
    }
}