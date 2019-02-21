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

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _IKhoVatTuRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
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
            var id = await _IKhoVatTuRepository.CheckTonTai(khoVatTuViewModel.TenKho);
            if (id == -1)
            {
                var result = await _IKhoVatTuRepository.InsertAsync(khoVatTuViewModel);
                return Ok(result);
            }
            else
            {
                return Ok(-1); // đã tồn tại không cho thêm
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _IKhoVatTuRepository.DeleteAsync(id);
            return Ok(result);
        }
    }
}