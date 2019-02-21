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
    public class NuocSanXuatController : BaseController
    {
        INuocSanXuatRepository _INuocSanXuatRepository;
        public NuocSanXuatController(INuocSanXuatRepository INuocSanXuatRepository)
        {
            _INuocSanXuatRepository = INuocSanXuatRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _INuocSanXuatRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _INuocSanXuatRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var models = await _INuocSanXuatRepository.GetByIdAsync(id);
            return Ok(models);
        }

        [HttpPut]
        public async Task<IActionResult> Update(NuocSanXuatViewModel nuocSanXuatViewModel)
        {
            var result = await _INuocSanXuatRepository.UpdateAsync(nuocSanXuatViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(NuocSanXuatViewModel nuocSanXuatViewModel)
        {
            var id = await _INuocSanXuatRepository.CheckTonTai(nuocSanXuatViewModel.TenNuoc);
            if (id == -1)
            {
                var result = await _INuocSanXuatRepository.InsertAsync(nuocSanXuatViewModel);
                return Ok(result);
            }
            else
            {
                if (await _INuocSanXuatRepository.GetStatus(id) == true)
                {
                    return Ok(-1); // đã tồn tại không cho thêm
                }
                else
                {
                    nuocSanXuatViewModel.MaNuoc = id;
                    var result = await _INuocSanXuatRepository.UpdateAsync(nuocSanXuatViewModel);
                    var resultChange = _INuocSanXuatRepository.ChangStatus(id);
                    return Ok(result);
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _INuocSanXuatRepository.DeleteAsync(id);
            return Ok(result);
        }
    }
}