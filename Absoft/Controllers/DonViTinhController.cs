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
            var models = await _donViTinhRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var models = await _donViTinhRepository.GetById(id);
            return Ok(models);
        }
        [HttpPut]
        public async Task<IActionResult> Update(DonViTinhViewModel donViTinhViewModel)
        {
            var result = await _donViTinhRepository.UpdateAsync(donViTinhViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(DonViTinhViewModel donViTinhViewModel)
        {
            var result = await _donViTinhRepository.InsertAsync(donViTinhViewModel);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _donViTinhRepository.IsDelete(id);
            return Ok(result);
        }
    }
}
