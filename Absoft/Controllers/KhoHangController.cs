using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Absoft.Controllers
{
    public class KhoHangController : BaseController
    {

        IKhoHangRepository _IKhoHangRepository;

        public KhoHangController(IKhoHangRepository IKhoHangRepository)
        {
            _IKhoHangRepository = IKhoHangRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _IKhoHangRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _IKhoHangRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("getByMaKho/{id}")]
        public async Task<IActionResult> GetByMaKho(int id)
        {
            var models = await _IKhoHangRepository.GetByMaKho(id);
            return Ok(models);
        }
        [HttpGet("getByMaPhieuNhap/{id}")]
        public async Task<IActionResult> GetByMaPhieuNhap(int id)
        {
            var models = await _IKhoHangRepository.GetByMaPhieuNhap(id);
            return Ok(models);
        }
        [HttpGet("getByMaVatTu/{id}")]
        public async Task<IActionResult> GetByMaVatTu(int id)
        {
            var models = await _IKhoHangRepository.GetByMaVatTu(id);
            return Ok(models);
        }
        [HttpGet("getTongTon/{maVT}/{maKho}")]
        public async Task<IActionResult> GetTongTon(int maVT, int maKho)
        {
            var models = await _IKhoHangRepository.GetTongTon(maVT, maKho);
            return Ok(models);
        }

        [HttpGet("getTotalCount")]
        public async Task<IActionResult> GetTotalCount()
        {
            var result = await _IKhoHangRepository.GetTotalCountAsync();
            return Ok(result);
        }
    }
}