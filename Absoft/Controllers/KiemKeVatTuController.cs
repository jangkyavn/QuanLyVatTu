using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Absoft.Controllers
{
    public class KiemKeVatTuController : BaseController
    {
        IKiemKeVatTuRepository _IKiemKeVatTuRepository;
        IKiemKeChiTietRepository _IKiemKeChiTietRepository;
        public KiemKeVatTuController(IKiemKeVatTuRepository IKiemKeVatTuRepository, IKiemKeChiTietRepository IKiemKeChiTietRepository)
        {
            _IKiemKeVatTuRepository = IKiemKeVatTuRepository;
            _IKiemKeChiTietRepository = IKiemKeChiTietRepository;
        }
        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _IKiemKeVatTuRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }
        [HttpDelete("{maPKK}")]
        public async Task<IActionResult> DeleteAsync(int MaPKK)
        {
            var models = await _IKiemKeVatTuRepository.DeleteAsync(MaPKK);
            return Ok(models);
        }
        [HttpPost("insertKiemKeVatTuAsync")]
        public async Task<IActionResult> InsertAsync(KiemKeVatTuViewModel model)
        {
            var models = await _IKiemKeVatTuRepository.InsertAsync(model);
            return Ok(models);
        }
        [HttpPut("updateKiemKeVatTuAsync")]
        public async Task<IActionResult> UpdateAsync(KiemKeVatTuViewModel model)
        {
            var models = await _IKiemKeVatTuRepository.UpdateAsync(model);
            return Ok(models);
        }
        [HttpDelete("deleteKiemKeChiTietAsync/{maPKK}/{maPN}/{maVT}/{maKho}")]
        public async Task<IActionResult> DeleteXuatChiTietAsync(int? maPKK, int? maPN, int? maVT, int? maKho)
        {
            var models = await _IKiemKeChiTietRepository.DeleteKiemKeChiTietAsync(maPKK.Value, maPN.Value, maVT.Value, maKho.Value);            
            return Ok(models);
        }
    }
}