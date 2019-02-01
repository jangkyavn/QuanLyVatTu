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
            var result = await _ILoaiVatTuRepository.InsertAsync(LoaiVatTuViewModel);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ILoaiVatTuRepository.IsDelete(id);
            return Ok(result);
        }
    }
}