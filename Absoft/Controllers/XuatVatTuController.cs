using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Absoft.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Absoft.Controllers
{
    public class XuatVatTuController : BaseController
    {
        IXuatVatTuRepository _IXuatVatTuRepository;
        public XuatVatTuController(IXuatVatTuRepository IXuatVatTuRepository)
        {
            _IXuatVatTuRepository = IXuatVatTuRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _IXuatVatTuRepository.GetAllAsync();
            return Ok(models);
        }
        [HttpGet("GetByMaPNAsync")]
        public async Task<IActionResult> GetByMaPNAsync(int maPN)
        {
            var models = await _IXuatVatTuRepository.GetByMaPNAsync(maPN);
            return Ok(models);
        }
        [HttpGet("GetDetailAsync")]
        public async Task<IActionResult> GetDetailAsync(int maPX)
        {
            var models = await _IXuatVatTuRepository.GetDetailAsync(maPX);
            return Ok(models);
        }
    }
}