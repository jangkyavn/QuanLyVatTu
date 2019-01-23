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
            var result = await _INguonCungCapRepository.InsertAsync(nguonCungCapViewModel);
            return Ok(result);
        }
    }
}