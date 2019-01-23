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
            var result = await _INuocSanXuatRepository.InsertAsync(nuocSanXuatViewModel);
            return Ok(result);
        }
    }
}