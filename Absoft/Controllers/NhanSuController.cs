using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Absoft.Controllers
{
    public class NhanSuController : BaseController
    {
        INhanSuRepository _INhanSuRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public NhanSuController(INhanSuRepository INhanSuRepository, IHostingEnvironment hostingEnvironment)
        {
            _INhanSuRepository = INhanSuRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _INhanSuRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _INhanSuRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var models = await _INhanSuRepository.GetByIdAsync(id);
            return Ok(models);
        }

        [HttpPut]
        public async Task<IActionResult> Update(NhanSuViewModel NhanSuViewModel)
        {
            var result = await _INhanSuRepository.UpdateAsync(NhanSuViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(NhanSuViewModel NhanSuViewModel)
        {
            var id = await _INhanSuRepository.CheckTonTai(NhanSuViewModel.HoTen);
            if (id == -1)
            {
                var result = await _INhanSuRepository.InsertAsync(NhanSuViewModel);
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
            var result = await _INhanSuRepository.DeleteAsync(id);
            return Ok(result);
        }
        [HttpDelete("DeleteAllAsync/{strIds}")]
        public async Task<IActionResult> DeleteAllAsync(string strIds)
        {
            // strids [1,2,3,4,4,5,56,6]
            List<int> listId = JsonConvert.DeserializeObject<List<int>>(strIds);
            var result = await _INhanSuRepository.DeleteAllAsync(listId);
            return Ok(result);
        }
        [HttpGet("loadCities")]
        public IActionResult LoadCities()
        {
            var models = _INhanSuRepository.LoadCities();
            return new OkObjectResult(models);
        }
        [HttpGet("loadDistricts/{cityId}")]
        public IActionResult LoadDistricts(int? cityId)
        {
            var models = _INhanSuRepository.LoadDistricts(cityId.Value);
            return new OkObjectResult(models);
        }
        [HttpGet("loadNations")]
        public IActionResult LoadNations()
        {
            var models = _INhanSuRepository.LoadNations();
            return new OkObjectResult(models);
        }
    }
}