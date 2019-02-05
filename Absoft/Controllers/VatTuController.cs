using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var result = await _IVatTuRepository.InsertAsync(VatTuViewModel);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _IVatTuRepository.IsDelete(id);
            return Ok(result);
        }
    }
}