using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Absoft.Controllers
{
    public class ThanhLyVatTuController : BaseController
    {
        IThanhLyVatTuRepository __IThanhLyVatTuRepository;
        IThanhLyChiTietRepository _IThanhLyChiTietRepository;
        public ThanhLyVatTuController(IThanhLyChiTietRepository IThanhLyChiTietRepository, IThanhLyVatTuRepository _IThanhLyVatTuRepository)
        {
            _IThanhLyChiTietRepository = IThanhLyChiTietRepository;
            __IThanhLyVatTuRepository = _IThanhLyVatTuRepository;
        }
        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await __IThanhLyVatTuRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }      
        [HttpGet("GetDetail/{maPTL}")]
        public async Task<IActionResult> GetDetailAsync(int maPTL)
        {
            var models = await __IThanhLyVatTuRepository.GetDetailAsync(maPTL);
            return Ok(models);
        }
        [HttpGet("GetByMaKho/{maKho}")]
        public async Task<IActionResult> GetByMaKhoAsync(int maKho)
        {
            var models = await __IThanhLyVatTuRepository.GetByMaKhoAsync(maKho);
            return Ok(models);
        }
        [HttpGet("GetListVTByMaPN/{maPN}")]
        public async Task<IActionResult> GetListVTByMaPNAsync(int maPN)
        {
            var models = await __IThanhLyVatTuRepository.GetListVTByMaPNAsync(maPN);
            return Ok(models);
        }
        [HttpGet("GetListVatTuByMaKho/{maKho}")]
        public async Task<IActionResult> GetListVatTuByMaKhoAsync(int maKho)
        {
            var models = await __IThanhLyVatTuRepository.GetListVatTuByMaKhoAsync(maKho);
            return Ok(models);
        }
        [HttpGet("GetListNhapChiTietByMaVT/{maVT}")]
        public async Task<IActionResult> GetListNhapChiTietByMaVTAsync(int maVT)
        {
            var models = await __IThanhLyVatTuRepository.GetListNhapChiTietByMaVTAsync(maVT);
            return Ok(models);
        }
        //[HttpPost]
        //public async Task<IActionResult> Insert(XuatVatTuParams xuatVatTuParams)
        //{
        //    var models = await _IThanhLyVatTuRepository.InsertAsync(xuatVatTuParams.mxuatvattu, xuatVatTuParams.listxuatchitiet);
        //    return Ok(models);
        //}
        [HttpPost("InserThanhLyVatTu")]
        public async Task<IActionResult> InserThanhLyVatTu(ThanhLyVatTuViewModel model)
        {
            var models = await __IThanhLyVatTuRepository.InserThanhLyVatTu(model);
            return Ok(models);
        }
        [HttpPost("InsertThanhLyChiTiet")]
        public async Task<IActionResult> InsertThanhLyChiTiet(ThanhLyChiTietlParams param)
        {
            var models = await _IThanhLyChiTietRepository.InsertThanhLyChiTiet(param.thanhlychitiet, param.maPTL);
            return Ok(models);
        }
        [HttpDelete("{maPTL}")]
        public async Task<IActionResult> DeleteAsync(int maPTL)
        {
            var models = await __IThanhLyVatTuRepository.DeleteAsync(maPTL);
            return Ok(models);
        }
        [HttpDelete("deleteThanhLyChiTietAsync/{maPTL}/{maPN}/{maVT}/{maKho}")]
        public async Task<IActionResult> DeleteXuatChiTietAsync(int? maPTL, int? maPN, int? maVT, int? maKho)
        {
            var models = false;
            if (maPTL != null && maPN != null && maVT != null && maKho != null)
            {
                if (await _IThanhLyChiTietRepository.CheckDeleteThanhLyChiTietAsync(maPTL.Value, maPN.Value, maVT.Value, maKho.Value) == true)
                {
                    models = await _IThanhLyChiTietRepository.DeleteThanhLyChiTietAsync(maPTL.Value, maPN.Value, maVT.Value, maKho.Value);
                }
            }
            return Ok(models);
        }
        [HttpDelete("deleteAllThanhLyChiTiet/{maPTL}/{maPN}/{maVT}/{maKho}")]
        public async Task<IActionResult> deleteAllXuatChiTiet(int? maPTL, int? maPN, int? maVT, int? maKho)
        {
            var models = false;
            if (maPTL != null && maPN != null && maVT != null && maKho != null)
            {
                if (await _IThanhLyChiTietRepository.CheckDeleteThanhLyChiTietAsync(maPTL.Value, maPN.Value, maVT.Value, maKho.Value) == true)
                {
                    models = await _IThanhLyChiTietRepository.deleteAllThanhLyChiTiet(maPTL.Value, maPN.Value, maVT.Value, maKho.Value);
                }
            }
            return Ok(models);
        }      

        [HttpPut("updateThanhLyVatTuAsync")]
        public async Task<IActionResult> UpdateThanhLyVatTuAsync(ThanhLyVatTuViewModel thanhLyVatTuViewModel)
        {
            var models = await __IThanhLyVatTuRepository.UpdateThanhLyVatTuAsync(thanhLyVatTuViewModel);
            return Ok(models);
        }
        [HttpPut("updateThanhLyChiTietAsync")]
        public async Task<IActionResult> UpdateThanhLyChiTietAsync(ThanhLyChiTietlParams param)
        {
            var models = await _IThanhLyChiTietRepository.UpdateThanhLyChiTietAsync(param.thanhlychitiet, param.maPTL, param.maKho);
            return Ok(models);
        }
        [HttpGet("checkSoLuongThanhLyChiTietAsync/{maphieunhap}/{mavt}/{sl}")]
        public async Task<IActionResult> CheckSoLuongXuatChiTietAsync(int maphieunhap, int mavt, int sl)
        {
            var result = await _IThanhLyChiTietRepository.CheckSoLuongThanhLyChiTietAsync(maphieunhap, mavt, sl);
            return Ok(result);
        }

        [HttpGet("GetListByMaKho")]
        public async Task<IActionResult> GetListByMaKho([FromQuery]PagingParams pagingParams, int maKho, string ngaythanhly)
        {
            var result = await __IThanhLyVatTuRepository.GetListByMaKho(pagingParams, maKho, ngaythanhly);
            Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
            return Ok(result.Items);
        }

        [HttpGet("getThanhLyChiTiet{maptl}/{mapn}/{mavt}")]
        public async Task<IActionResult> GetThanhLyChiTiet(int maptl, int mapn, int mavt)
        {
            var result = await __IThanhLyVatTuRepository.GetThanhLyChiTiet(maptl, mapn, mavt);
            return Ok(result);
        }
        [HttpGet("CheckTonTaiVTChitiet/{maptl}/{maphieunhap}/{mavt}")]
        public async Task<IActionResult> CheckTonTaiVTChitiet(int maptl, int maphieunhap, int mavt)
        {
            var result = await _IThanhLyChiTietRepository.CheckTonTaiVTChitiet(maptl, maphieunhap, mavt);
            return Ok(result);
        }     
    }
}