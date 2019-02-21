using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Absoft.Controllers
{
    public class DonViTinhController : BaseController
    {
        IDonViTinhRepository _donViTinhRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public DonViTinhController(IDonViTinhRepository donViTinhRepository, IHostingEnvironment hostingEnvironment)
        {
            _donViTinhRepository = donViTinhRepository;
            _hostingEnvironment = hostingEnvironment;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _donViTinhRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _donViTinhRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
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
            var id = await _donViTinhRepository.CheckTonTai(donViTinhViewModel.TenDVT);
            if (id == -1)
            {
                var result = await _donViTinhRepository.InsertAsync(donViTinhViewModel);
                return Ok(result);
            }
            else
            {
                return Ok(-1);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _donViTinhRepository.DeleteAsync(id);
            return Ok(result);
        }
        [HttpPost]
        [Route("ImportDVT")]
        public async Task<IActionResult> ImportDVT(IList<IFormFile> files)
        {
            var result = await _donViTinhRepository.ImportDVT(files);
            return Ok(result);
        }
        [HttpGet]
        [Route("ExportDVT")]
        public IActionResult ExportDVT()
        {
            var result = _donViTinhRepository.ExportDVT();
            return Ok(result);
        }
    }
}
