using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Absoft.Controllers
{
    public class XuatVatTuController : BaseController
    {
        IXuatVatTuRepository _IXuatVatTuRepository;
        IXuatChiTietRepository _IXuatChiTietRepository;
        public XuatVatTuController(IXuatVatTuRepository IXuatVatTuRepository, IXuatChiTietRepository IXuatChiTietRepository)
        {
            _IXuatVatTuRepository = IXuatVatTuRepository;
            _IXuatChiTietRepository = IXuatChiTietRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _IXuatVatTuRepository.GetAllAsync();
            return Ok(models);
        }
        [HttpGet("GetByMaPN/{maPN}")]
        public async Task<IActionResult> GetByMaPNAsync(int maPN)
        {
            var models = await _IXuatVatTuRepository.GetByMaPNAsync(maPN);
            return Ok(models);
        }
        [HttpGet("GetDetail/{maPX}")]
        public async Task<IActionResult> GetDetailAsync(int maPX)
        {
            var models = await _IXuatVatTuRepository.GetDetailAsync(maPX);
            return Ok(models);
        }
        [HttpGet("GetByMaKho/{maKho}")]
        public async Task<IActionResult> GetByMaKhoAsync(int maKho)
        {
            var models = await _IXuatVatTuRepository.GetByMaKhoAsync(maKho);
            return Ok(models);
        }
        [HttpGet("GetListVTByMaPN/{maPN}")]
        public async Task<IActionResult> GetListVTByMaPNAsync(int maPN)
        {
            var models = await _IXuatVatTuRepository.GetListVTByMaPNAsync(maPN);
            return Ok(models);
        }
        [HttpGet("GetListVatTuByMaKho/{maKho}")]
        public async Task<IActionResult> GetListVatTuByMaKhoAsync(int maKho)
        {
            var models = await _IXuatVatTuRepository.GetListVatTuByMaKhoAsync(maKho);
            return Ok(models);
        }
        [HttpGet("GetListNhapChiTietByMaVT/{maVT}")]
        public async Task<IActionResult> GetListNhapChiTietByMaVTAsync(int maVT)
        {
            var models = await _IXuatVatTuRepository.GetListNhapChiTietByMaVTAsync(maVT);
            return Ok(models);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(XuatVatTuParams xuatVatTuParams)
        {
            var models = await _IXuatVatTuRepository.InsertAsync(xuatVatTuParams.mxuatvattu, xuatVatTuParams.listxuatchitiet);
            return Ok(models);
        }
        [HttpDelete("{maPX}")]
        public async Task<IActionResult> DeleteAsync(int maPX)
        {
            var models = await _IXuatVatTuRepository.DeleteAsync(maPX);
            return Ok(models);
        }
        [HttpDelete("deleteXuatChiTiet/{maPX}/{maPN}/{maVT}/{maKho}")]
        public async Task<IActionResult> DeleteXuatChiTietAsync(int? maPX, int? maPN, int? maVT, int? maKho)
        {
            var models = false;
            if (maPX != null && maPN != null && maVT != null && maKho != null)
            {
                if (await _IXuatChiTietRepository.CheckDeleteXuatChiTietAsync(maPX.Value, maPN.Value, maVT.Value, maKho.Value) == true)
                {
                    models = await _IXuatChiTietRepository.DeleteXuatChiTietAsync(maPX.Value, maPN.Value, maVT.Value, maKho.Value);
                }
            }
            return Ok(models);
        }
        [HttpDelete("deleteAllXuatChiTiet/{maPX}/{maPN}/{maVT}/{maKho}")]
        public async Task<IActionResult> deleteAllXuatChiTiet(int? maPX, int? maPN, int? maVT, int? maKho)
        {
            var models = false;
            if (maPX != null && maPN != null && maVT != null && maKho != null)
            {
                if (await _IXuatChiTietRepository.CheckDeleteXuatChiTietAsync(maPX.Value, maPN.Value, maVT.Value, maKho.Value) == true)
                {
                    models = await _IXuatChiTietRepository.deleteAllXuatChiTiet(maPX.Value, maPN.Value, maVT.Value, maKho.Value);
                }
            }
            return Ok(models);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(XuatVatTuParams xuatVatTuParams)
        {
            var models = await _IXuatVatTuRepository.UpdateAsync(xuatVatTuParams.mxuatvattu, xuatVatTuParams.listxuatchitiet);
            return Ok(models);
        }
        [HttpGet("CheckSoLuongXuatChiTietAsync/{maphieunhap}/{mavt}/{sl}")]
        public async Task<IActionResult> CheckSoLuongXuatChiTietAsync(int maphieunhap, int mavt, int sl)
        {
            var result = await _IXuatChiTietRepository.CheckSoLuongXuatChiTietAsync(maphieunhap, mavt, sl);
            return Ok(result);
        }
    }
}