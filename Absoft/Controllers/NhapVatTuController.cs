using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Absoft.Controllers
{
    public class NhapVatTuController : BaseController
    {
        INhapVatTuRepository _INhapVatTuRepository;
        INhapChiTietRepository _INhapChiTietRepository;

        public NhapVatTuController(INhapVatTuRepository INhapVatTuRepository, INhapChiTietRepository INhapChiTietRepository)
        {
            _INhapVatTuRepository = INhapVatTuRepository;
            _INhapChiTietRepository = INhapChiTietRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _INhapVatTuRepository.GetAllAsync();
            return Ok(models);
        }
        //[HttpPost]
        //public async Task<IActionResult> Insert(NhapVatTuParams nhapVatTuParams)
        //{
        //    var result = await _INhapVatTuRepository.InsertAsync(nhapVatTuParams.mnhapvattu, nhapVatTuParams.listnhapchitiet);
        //    return Ok(result);
        //}
        [HttpPost("insertNhapVatTuAsync")]
        public async Task<IActionResult> InsertNhapVatTuAsync(NhapVatTuViewModel mnhapvattu)
        {
            var result = await _INhapVatTuRepository.InsertNhapVatTuAsync(mnhapvattu);
            return Ok(result);
        }
        [HttpPost("insertChiTietAsync")]
        public async Task<IActionResult> InsertChiTietAsync(NhapChiTietViewModel mnhapchitiet, int maphieunhap)
        {
            var result = await _INhapChiTietRepository.InsertChiTietAsync(mnhapchitiet, maphieunhap);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(NhapVatTuParams nhapVatTuParams)
        {
            var result = await _INhapVatTuRepository.UpdateAsync(nhapVatTuParams.mnhapvattu, nhapVatTuParams.listnhapchitiet);
            // return 1 thanh cong
            // return -1 so luong ton kho am
            // return 0 loi update
            return Ok(result);
        }
        [HttpDelete("{maPN}")]
        public async Task<IActionResult> Delete(int maPN)
        {
            var result = await _INhapVatTuRepository.DeleteAsync(maPN);
            return Ok(result);
        }
        [HttpDelete("removeNhapchitiet/{mapn}/{mavt}/{makho}")]
        public async Task<IActionResult> RemoveChiTiet(int mapn, int? mavt, int makho)
        {
            var result = false;
            if (mavt != null)
            {
                if (await _INhapChiTietRepository.CheckDeleteNhapChiTietAsync(mapn, mavt.Value, makho) == true)
                {
                    result = await _INhapChiTietRepository.DeleteNhapChiTietAsync(mapn, mavt.Value, makho);
                }
            }
            return Ok(result); // false vat tu da xuat khong duoc xoa
        }
        [HttpDelete("removeallNhapchitiet/{mapn}/{makho}")]
        public async Task<IActionResult> removeallNhapchitiet(int mapn, int makho)
        {
            var result = await _INhapChiTietRepository.removeallNhapchitiet(mapn, makho);
            return Ok(result); // false vat tu da xuat khong duoc xoa
        }
        [HttpGet("{maPN}")]
        public async Task<IActionResult> GetDetailAsync(int maPN)
        {
            var result = await _INhapVatTuRepository.GetDetailAsync(maPN);
            return Ok(result);
        }
        [HttpGet("CheckStatus/{mapn}/{mavt}/{makho}")]
        public async Task<IActionResult> CheckStatus(int mapn, int mavt, int makho)
        {
            var result = await _INhapChiTietRepository.CheckStatus(mapn, mavt, makho);
            // result == true cho xóa, false không cho xóa
            return Ok(result);
        }
        [HttpGet("CheckSoLuongNhapChiTietAsync/{maphieunhap}/{makho}/{mavt}/{sl}")]
        public async Task<IActionResult> CheckSoLuongNhapChiTietAsync(int maphieunhap, int makho, int mavt, int sl)
        {
            var result = await _INhapChiTietRepository.CheckSoLuongNhapChiTietAsync(maphieunhap, makho, mavt, sl);
            return Ok(result);
        }
    }
}