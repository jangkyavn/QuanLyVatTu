using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Absoft.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}