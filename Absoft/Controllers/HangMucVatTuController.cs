using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Absoft.Controllers
{
    public class HangMucVatTuController : BaseController
    {
        IHangMucVatTuRepository _hangMucVatTuRepository;
        public HangMucVatTuController(IHangMucVatTuRepository hangMucVatTuRepository)
        {
            _hangMucVatTuRepository = hangMucVatTuRepository;
        }       

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _hangMucVatTuRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _hangMucVatTuRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var models = await _hangMucVatTuRepository.GetByIdAsync(id);
            return Ok(models);
        }

        [HttpPut]
        public async Task<IActionResult> Update(HangMucVatTuViewModel hangMucVatTuViewModel)
        {
            var result = await _hangMucVatTuRepository.UpdateAsync(hangMucVatTuViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(HangMucVatTuViewModel hangMucVatTuViewModel)
        {
            var result = await _hangMucVatTuRepository.InsertAsync(hangMucVatTuViewModel);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _hangMucVatTuRepository.IsDelete(id);
            return Ok(result);
        }
    }
}