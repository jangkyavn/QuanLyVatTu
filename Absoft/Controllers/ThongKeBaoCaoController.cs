using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            var models = await _IThongKeRepository.ThongKeNhap(thongKeParams);
            return Ok(models);
        }
        [HttpGet("ThongKeXuat")]
        public async Task<IActionResult> ThongKeXuat([FromQuery]ThongKeParams thongKeParams)
        {
            var models = await _IThongKeRepository.ThongKeXuat(thongKeParams);
            return Ok(models);
        }
        [HttpGet("ThongKeThanhLy")]
        public async Task<IActionResult> ThongKeThanhLy([FromQuery]ThongKeParams thongKeParams)
        {
            var models = await _IThongKeRepository.ThongKeThanhLy(thongKeParams);
            return Ok(models);
        }
        [HttpGet("ThongKeNhapXuatTon")]
        public async Task<IActionResult> ThongKeNhapXuatTon([FromQuery]ThongKeParams thongKeParams)
        {
            var models = await _IThongKeRepository.ThongKeNhapXuatTon(thongKeParams);
            return Ok(models);
        }
    }
}