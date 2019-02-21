using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Absoft.Controllers
{

    public class NguonCungCapController : BaseController
    {
        INguonCungCapRepository _INguonCungCapRepository;
        public NguonCungCapController(INguonCungCapRepository INguonCungCapRepository)
        {
            _INguonCungCapRepository = INguonCungCapRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _INguonCungCapRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _INguonCungCapRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var models = await _INguonCungCapRepository.GetByIdAsync(id);
            return Ok(models);
        }

        [HttpPut]
        public async Task<IActionResult> Update(NguonCungCapViewModel nguonCungCapViewModel)
        {
            var result = await _INguonCungCapRepository.UpdateAsync(nguonCungCapViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(NguonCungCapViewModel nguonCungCapViewModel)
        {
            var id = await _INguonCungCapRepository.CheckTonTai(nguonCungCapViewModel.TenNguon);
            if (id == -1)
            {
                var result = await _INguonCungCapRepository.InsertAsync(nguonCungCapViewModel);
                return Ok(result);
            }
            else
            {
                if (await _INguonCungCapRepository.GetStatus(id) == true)
                {
                    return Ok(-1); // đã tồn tại không cho thêm
                }
                else
                {
                    nguonCungCapViewModel.MaNguon = id;
                    var result = await _INguonCungCapRepository.UpdateAsync(nguonCungCapViewModel);
                    var resultChange = _INguonCungCapRepository.ChangStatus(id);
                    return Ok(result);
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _INguonCungCapRepository.DeleteAsync(id);
            return Ok(result);
        }
    }
}