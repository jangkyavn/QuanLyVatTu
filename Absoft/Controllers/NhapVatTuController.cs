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
        [HttpPost]
        public async Task<IActionResult> Insert(NhapVatTuParams nhapVatTuParams)
        {
            var result = await _INhapVatTuRepository.InsertAsync(nhapVatTuParams.mnhapvattu, nhapVatTuParams.listnhapchitiet);
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
        [HttpDelete]
        public async Task<IActionResult> Delete(NhapVatTuParams nhapVatTuParams)
        {            
            var result = await _INhapVatTuRepository.DeleteAsync(nhapVatTuParams.mnhapvattu, nhapVatTuParams.listnhapchitiet);         
            //foreach(var item in nhapVatTuParams.listnhapchitiet)
            //{
            //    var khohang = db.KhoHangs.FirstOrDefault(x => x.MaKho == makho && x.MaPhieuNhap == maphieunhap && x.MaVatTu == mnhapchitiet.MaVatTu);
            //}
            return Ok(result);
        }
        [HttpDelete("{RemoveChiTiet/}")]
        public async Task<IActionResult> RemoveChiTiet(NhapChiTietParams nhapChiTiet)
        {            
            var result = await _INhapChiTietRepository.DeleteNhapChiTietAsync(nhapChiTiet.nhapchitiet, nhapChiTiet.maphieunhap,nhapChiTiet.makho);
            return Ok(result);
        }
    }
}