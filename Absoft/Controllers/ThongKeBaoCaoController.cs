using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Absoft.Controllers
{
    public class ThongKeBaoCaoController : BaseController
    {
        IThongKeRepository _IThongKeRepository;
        public ThongKeBaoCaoController(IThongKeRepository IThongKeRepository)
        {
            _IThongKeRepository = IThongKeRepository;
        }
     
        [HttpGet("ThongKeNhap")]
        public async Task<IActionResult> ThongKeNhap([FromQuery]ThongKeParams thongKeParams)
        {
            var models = await _IThongKeRepository.ThongKeNhap(thongKeParams.fromDate, thongKeParams.toDate, thongKeParams.maLoaiVT, thongKeParams.maHM, thongKeParams.maKho);
            return Ok(models);
        }
        [HttpGet("ThongKeXuat")]
        public async Task<IActionResult> ThongKeXuat([FromQuery]ThongKeParams thongKeParams)
        {
            var models = await _IThongKeRepository.ThongKeXuat(thongKeParams.fromDate, thongKeParams.toDate, thongKeParams.maLoaiVT, thongKeParams.maHM, thongKeParams.maKho);
            return Ok(models);
        }
        [HttpGet("ThongKeThanhLy")]
        public async Task<IActionResult> ThongKeThanhLy([FromQuery]ThongKeParams thongKeParams)
        {
            var models = await _IThongKeRepository.ThongKeThanhLy(thongKeParams.fromDate, thongKeParams.toDate, thongKeParams.maLoaiVT, thongKeParams.maHM, thongKeParams.maKho);
            return Ok(models);
        }
        [HttpGet("ThongKeNhapXuatTon")]
        public async Task<IActionResult> ThongKeNhapXuatTon([FromQuery]ThongKeParams thongKeParams)
        {
            var models = await _IThongKeRepository.ThongKeNhapXuatTon(thongKeParams.fromDate, thongKeParams.toDate, thongKeParams.maLoaiVT, thongKeParams.maHM, thongKeParams.maKho);
            return Ok(models);
        }
    }
}