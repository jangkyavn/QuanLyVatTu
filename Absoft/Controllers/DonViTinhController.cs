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
    public class DonViTinhController : BaseController
    {
        IDonViTinhRepository _donViTinhRepository;

        public DonViTinhController(IDonViTinhRepository donViTinhRepository)
        {
            _donViTinhRepository = donViTinhRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var donViTinhs = await _donViTinhRepository.GetAllAsync();
            return Ok(donViTinhs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var donViTinh = await _donViTinhRepository.GetById(id);
            return Ok(donViTinh);
        }

        [HttpPut]
        public async Task<IActionResult> Update(DonViTinhViewModel donViTinhViewModel)
        {
            var result = await _donViTinhRepository.UpdateAsync(donViTinhViewModel);
            return Ok(result);
        }
    }
}
