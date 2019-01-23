﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Absoft.Controllers
{   
    public class KhoVatTuController : BaseController
    {
        IKhoVatTuRepository _IKhoVatTuRepository;
        public KhoVatTuController(IKhoVatTuRepository IKhoVatTuRepository)
        {
            _IKhoVatTuRepository = IKhoVatTuRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _IKhoVatTuRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var models = await _IKhoVatTuRepository.GetByIdAsync(id);
            return Ok(models);
        }

        [HttpPut]
        public async Task<IActionResult> Update(KhoVatTuViewModel khoVatTuViewModel)
        {
            var result = await _IKhoVatTuRepository.UpdateAsync(khoVatTuViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(KhoVatTuViewModel khoVatTuViewModel)
        {
            var result = await _IKhoVatTuRepository.InsertAsync(khoVatTuViewModel);
            return Ok(result);
        }
    }
}