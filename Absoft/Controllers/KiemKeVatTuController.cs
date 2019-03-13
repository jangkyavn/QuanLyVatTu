using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Absoft.Data;
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
        [HttpGet("getDetailById/{maPKK}")]
        public async Task<IActionResult> GetAllPaging(int maPKK)
        {
            var result = await _IKiemKeVatTuRepository.GetDetailAsync(maPKK);
            return Ok(result);
        }
        [HttpDelete("{maPKK}")]
        public async Task<IActionResult> DeleteAsync(int MaPKK)
        {
            var models = await _IKiemKeVatTuRepository.DeleteAsync(MaPKK);
            if (models == true)
            {
                // goi ham khoa tat ca phieu nhap, xuat, thanh ly
                var entity = await _IKiemKeVatTuRepository.GetById(MaPKK);
                var rs = await _IKiemKeVatTuRepository.UnablePnPxPtl(entity.NgayKiemKe);
                if (rs == true) return Ok(true);
                else return Ok(false);
            }
            else return Ok(false);
            // return Ok(models);
        }
        [HttpPost("insertKiemKeVatTuAsync")]
        public async Task<IActionResult> InsertAsync(KiemKeVatTuViewModel model)
        {
            var models = await _IKiemKeVatTuRepository.InsertAsync(model);
            // neu return models = MaPhieuKK moi tao
            if (models > 0)
            {
                // goi ham khoa tat ca phieu nhap, xuat, thanh ly
                var rs = await _IKiemKeVatTuRepository.DisablePnPxPtl(model.NgayKiemKe);
                if (rs == true) return Ok(models);
                else return Ok(-1);
            }
            else return Ok(-1);

        }
        [HttpPut("updateKiemKeVatTuAsync")]
        public async Task<IActionResult> UpdateAsync(KiemKeVatTuViewModel model)
        {
            // _IKiemKeChiTietRepository.CheckExistChiTiet(maPKK); neu ton tai chi tiet khong cho sua ngaykiemke
            var models = await _IKiemKeVatTuRepository.UpdateAsync(model);
            return Ok(models);
        }
        [HttpDelete("deleteKiemKeChiTietAsync/{maPKK}/{maPN}/{maVT}/{maKho}")]
        public async Task<IActionResult> DeleteXuatChiTietAsync(int? maPKK, int? maPN, int? maVT, int? maKho)
        {
            var models = await _IKiemKeChiTietRepository.DeleteKiemKeChiTietAsync(maPKK.Value, maPN.Value, maVT.Value, maKho.Value);
            return Ok(models);
        }
        [HttpPost("inserKiemKeChiTietAsync")]
        public async Task<IActionResult> InserKiemKeChiTietAsync(KiemKeChiTietParams param)
        {
            var models = await _IKiemKeChiTietRepository.InserKiemKeChiTietAsync(param.mkiemkechitiet, param.maPKK.Value, param.maKho.Value);
            return Ok(models);
        }
        [HttpPut("updateKiemKeChiTietAsync")]
        public async Task<IActionResult> UpdateKiemKeChiTietAsync(KiemKeChiTietParams param)
        {
            var models = await _IKiemKeChiTietRepository.UpdateKiemKeChiTietAsync(param.mkiemkechitiet, param.maPKK.Value, param.maKho.Value);
            return Ok(models);
        }
        [HttpGet("getSoLuongTheoDoi/{maKho}/{maPN}/{maVT}")]
        public async Task<IActionResult> GetSoLuongTheoDoi(int? maKho, int? maPN, int? maVT)
        {
            var result = await _IKiemKeChiTietRepository.GetSoLuongTheoDoi(maKho.Value, maPN.Value, maVT.Value);
            return Ok(result);
        }
        [HttpGet("checkExistChiTiet/{maPKK}")]
        public async Task<IActionResult> CheckExistChiTiet(int maPKK)
        {
            var result = await _IKiemKeChiTietRepository.CheckExistChiTiet(maPKK);
            return Ok(result); // neu ton tai chi tiet khong cho sua ngay
        }
        [HttpGet("getListKho/{maKho}/{maPN}/{maVT}/{status}")]
        public async Task<IActionResult> GetListKho(PagingParams parginparam, int? maKho, int? maPN, int? maVT, bool status)
        {
            var result = await _IKiemKeVatTuRepository.GetListKho(parginparam, maKho.Value, maPN.Value, maVT.Value, status);
            return Ok(result);
        }
    }
}