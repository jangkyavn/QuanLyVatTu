using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Absoft.Controllers
{
    public class LoaiVatTuController : BaseController
    {
        ILoaiVatTuRepository _ILoaiVatTuRepository;
        public LoaiVatTuController(ILoaiVatTuRepository ILoaiVatTuRepository)
        {
            _ILoaiVatTuRepository = ILoaiVatTuRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _ILoaiVatTuRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _ILoaiVatTuRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailById(int id)
        {
            var models = await _ILoaiVatTuRepository.GetByIdAsync(id);
            return Ok(models);
        }
        [HttpGet("getListLoaiByMaHM/{maMH}")]
        public async Task<IActionResult> GetListLoaiByMaHM(int maMH)
        {
            var models = await _ILoaiVatTuRepository.GetListLoaiByMaHM(maMH);
            return Ok(models);
        }
        [HttpPut]
        public async Task<IActionResult> Update(LoaiVatTuViewModel LoaiVatTuViewModel)
        {
            var result = await _ILoaiVatTuRepository.UpdateAsync(LoaiVatTuViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(LoaiVatTuViewModel LoaiVatTuViewModel)
        {
            var id = await _ILoaiVatTuRepository.CheckTonTai(LoaiVatTuViewModel.TenLoai);
            if (id == -1)
            {
                var result = await _ILoaiVatTuRepository.InsertAsync(LoaiVatTuViewModel);
                return Ok(result);
            }
            else
            {
                if (await _ILoaiVatTuRepository.GetStatus(id) == true)
                {
                    return Ok(-1); // đã tồn tại không cho thêm
                }
                else
                {
                    var result = await _ILoaiVatTuRepository.UpdateAsync(LoaiVatTuViewModel);
                    var resultChange = _ILoaiVatTuRepository.ChangStatus(id);
                    return Ok(result);
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ILoaiVatTuRepository.IsDelete(id);
            return Ok(result);
        }
    }
}