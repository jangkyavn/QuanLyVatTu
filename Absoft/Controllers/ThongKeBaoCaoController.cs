using Absoft.Authorization;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Absoft.Controllers
{
    public class ThongKeBaoCaoController : BaseController
    {
        IThongKeRepository _IThongKeRepository;
        private readonly IAuthorizationService _authorizationService;

        public ThongKeBaoCaoController(IThongKeRepository IThongKeRepository,
            IAuthorizationService authorizationService)
        {
            _IThongKeRepository = IThongKeRepository;
            _authorizationService = authorizationService;
        }
     
        [HttpGet("ThongKeNhap")]
        public async Task<IActionResult> ThongKeNhap([FromQuery]ThongKeParams thongKeParams)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "THONG_KE_NHAP", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _IThongKeRepository.ThongKeNhap(thongKeParams);
            return Ok(models);
        }
        [HttpGet("ThongKeXuat")]
        public async Task<IActionResult> ThongKeXuat([FromQuery]ThongKeParams thongKeParams)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "THONG_KE_XUAT", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _IThongKeRepository.ThongKeXuat(thongKeParams);
            return Ok(models);
        }
        [HttpGet("ThongKeThanhLy")]
        public async Task<IActionResult> ThongKeThanhLy([FromQuery]ThongKeParams thongKeParams)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "THONG_KE_THANH_LY", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _IThongKeRepository.ThongKeThanhLy(thongKeParams);
            return Ok(models);
        }
        [HttpGet("ThongKeNhapXuatTon")]
        public async Task<IActionResult> ThongKeNhapXuatTon([FromQuery]ThongKeParams thongKeParams)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "THONG_KE_NHAP_XUAT_TON", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _IThongKeRepository.ThongKeNhapXuatTon(thongKeParams);
            return Ok(models);
        }
    }
}