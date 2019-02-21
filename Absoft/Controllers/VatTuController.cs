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
    public class VatTuController : BaseController
    {
        IVatTuRepository _IVatTuRepository;
        public VatTuController(IVatTuRepository IVatTuRepository)
        {
            _IVatTuRepository = IVatTuRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _IVatTuRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _IVatTuRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("getTotalCount")]
        public async Task<IActionResult> GetTotalCount()
        {
            var result = await _IVatTuRepository.GetTotalCountAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailById(int id)
        {
            var models = await _IVatTuRepository.GetByIdAsync(id);
            return Ok(models);
        }
        [HttpGet("getByMaLoaiVT/{maLoaiVT}")]
        public async Task<IActionResult> GetByMaLoaiVTAsync(int maLoaiVT)
        {
            var models = await _IVatTuRepository.GetByMaLoaiVTAsync(maLoaiVT);
            return Ok(models);
        }
        [HttpGet("getByMaDV/{maDVT}")]
        public async Task<IActionResult> GetByMaDVAsync(int maDVT)
        {
            var models = await _IVatTuRepository.GetByMaDVAsync(maDVT);
            return Ok(models);
        }
        [HttpPut]
        public async Task<IActionResult> Update(VatTuViewModel VatTuViewModel)
        {
            var result = await _IVatTuRepository.UpdateAsync(VatTuViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(VatTuViewModel VatTuViewModel)
        {
            var id = await _IVatTuRepository.CheckTonTai(VatTuViewModel.TenVT);
            if (id == -1)
            {
                var result = await _IVatTuRepository.InsertAsync(VatTuViewModel);
                return Ok(result);
            }
            else
            {
                if (await _IVatTuRepository.GetStatus(id) == true)
                {
                    return Ok(-1); // đã tồn tại không cho thêm
                }
                else
                {
                    VatTuViewModel.MaVatTu = id;
                    var result = await _IVatTuRepository.UpdateAsync(VatTuViewModel);
                    var resultChange = _IVatTuRepository.ChangStatus(id);
                    return Ok(result);
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _IVatTuRepository.DeleteAsync(id);
            return Ok(result);
        }
        [HttpGet("GetByMaHM/{maHM}")]
        public async Task<IActionResult> GetByMaHM(int maHM)
        {
            var models = await _IVatTuRepository.GetByMaHM(maHM);
            return Ok(models);
        }
        [HttpPost]
        [Route("ImportVT")]
        public async Task<IActionResult> ImportVT(IList<IFormFile> files)
        {
            var result = await _IVatTuRepository.ImportVT(files);
            return Ok(result);
        }
    }
}