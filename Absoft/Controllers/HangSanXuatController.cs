using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Absoft.Controllers
{   
    public class HangSanXuatController : BaseController
    {
        IHangSanXuatRepository _hangSanXuatRepository;
        public HangSanXuatController(IHangSanXuatRepository hangSanXuatRepository)
        {
            _hangSanXuatRepository = hangSanXuatRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _hangSanXuatRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _hangSanXuatRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var models = await _hangSanXuatRepository.GetByIdAsync(id);
            return Ok(models);
        }

        [HttpPut]
        public async Task<IActionResult> Update(HangSanXuatViewModel hangSanXuatViewModel)
        {
            var result = await _hangSanXuatRepository.UpdateAsync(hangSanXuatViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(HangSanXuatViewModel hangSanXuatViewModel)
        {
            var id = await _hangSanXuatRepository.CheckTonTai(hangSanXuatViewModel.TenHang);
            if (id == -1)
            {
                var result = await _hangSanXuatRepository.InsertAsync(hangSanXuatViewModel);
                return Ok(result);
            }
            else
            {
                if (await _hangSanXuatRepository.GetStatus(id) == true)
                {
                    return Ok(-1); // đã tồn tại không cho thêm
                }
                else
                {
                    hangSanXuatViewModel.MaHang = id;
                    var result = await _hangSanXuatRepository.UpdateAsync(hangSanXuatViewModel);
                    var resultChange = _hangSanXuatRepository.ChangStatus(id);
                    return Ok(result);
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _hangSanXuatRepository.IsDelete(id);
            return Ok(result);
        }
    }
}