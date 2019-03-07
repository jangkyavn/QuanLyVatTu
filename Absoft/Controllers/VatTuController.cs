using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absoft.Controllers
{
    public class VatTuController : BaseController
    {
        IVatTuRepository _IVatTuRepository;
        public VatTuController(IVatTuRepository IVatTuRepository)
        {
            _IVatTuRepository = IVatTuRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _IVatTuRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _IVatTuRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("getAllPagingWithTongTon")]
        public async Task<IActionResult> GetAllPagingWithTongTonAsync([FromQuery]PagingParams pagingParams)
        {
            var paged = await _IVatTuRepository.GetAllPagingWithTongTonAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("getTotalCount")]
        public async Task<IActionResult> GetTotalCount()
        {
            var result = await _IVatTuRepository.GetTotalCountAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailById(int id)
        {
            var models = await _IVatTuRepository.GetByIdAsync(id);
            return Ok(models);
        }
        [HttpGet("getByMaLoaiVT/{maLoaiVT}")]
        public async Task<IActionResult> GetByMaLoaiVTAsync(int maLoaiVT)
        {
            var models = await _IVatTuRepository.GetByMaLoaiVTAsync(maLoaiVT);
            return Ok(models);
        }
        [HttpGet("getByMaDV/{maDVT}")]
        public async Task<IActionResult> GetByMaDVAsync(int maDVT)
        {
            var models = await _IVatTuRepository.GetByMaDVAsync(maDVT);
            return Ok(models);
        }
        [HttpPut]
        public async Task<IActionResult> Update(VatTuViewModel VatTuViewModel)
        {
            var result = await _IVatTuRepository.UpdateAsync(VatTuViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(VatTuViewModel VatTuViewModel)
        {
            var id = await _IVatTuRepository.CheckTonTai(VatTuViewModel.TenVT);
            if (id == -1)
            {
                var result = await _IVatTuRepository.InsertAsync(VatTuViewModel);
                return Ok(result);
            }
            else
            {
                return Ok(-1); // đã tồn tại không cho thêm
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _IVatTuRepository.DeleteAsync(id);
            return Ok(result);
        }
        [HttpDelete("DeleteAllAsync/{strIds}")]
        public async Task<IActionResult> DeleteAllAsync(string strIds)
        {
            // strids [1,2,3,4,4,5,56,6]
            List<int> listId = JsonConvert.DeserializeObject<List<int>>(strIds);
            var result = await _IVatTuRepository.DeleteAllAsync(listId);
            return Ok(result);
        }
        [HttpGet("GetByMaHM/{maHM}")]
        public async Task<IActionResult> GetByMaHM(int maHM)
        {
            var models = await _IVatTuRepository.GetByMaHM(maHM);
            return Ok(models);
        }
        [HttpPost]
        [Route("ImportVT")]
        public async Task<IActionResult> ImportVT(IList<IFormFile> files)
        {
            var result = await _IVatTuRepository.ImportVT(files);
            return Ok(result);
        }
        [HttpPost]
        [Route("ExportVT")]
        public IActionResult ExportVT(List<VatTuViewModel> vatTuViewModels)
        {
            var result = _IVatTuRepository.ExportVT(vatTuViewModels);
            return Ok(result);
        }
        [HttpGet]
        [Route("DeleteFileVTAfterExport/{filename}")]
        public IActionResult DeleteFileVTAfterExport(string filename)
        {
            var result = _IVatTuRepository.DeleteFileVTAfterExport(filename);
            return Ok(result);
        }

        [HttpGet]
        [Route("ThongKeVatTuNhapByMaVT")]
        public async Task<IActionResult> ThongKeVatTuNhapByMaVT([FromQuery]PagingParams pagingParams, int Id)
        {
            var result = await _IVatTuRepository.ThongKeVatTuNhapByMaVT(pagingParams, Id);
            Response.AddPagination(result.pl.CurrentPage, result.pl.PageSize, result.pl.TotalCount, result.pl.TotalPages);
            return Ok(
                new
                {
                    result.pl.Items,
                    result.tongluong,
                    result.tongtien
                });
        }

        [HttpGet]
        [Route("ThongKeVatTuXuatpByMaVT")]
        public async Task<IActionResult> ThongKeVatTuXuatpByMaVT([FromQuery]PagingParams pagingParams, int Id)
        {
            var result = await _IVatTuRepository.ThongKeVatTuXuatpByMaVT(pagingParams, Id);
            Response.AddPagination(result.pl.CurrentPage, result.pl.PageSize, result.pl.TotalCount, result.pl.TotalPages);
            return Ok(
                new
                {
                    result.pl.Items,
                    result.tongluong,
                    result.tongtien
                });
        }

        [HttpGet("ThongKeVatTuThanhLyByMaVT")]
        public async Task<IActionResult> ThongKeVatTuThanhLyByMaVT([FromQuery]PagingParams pagingParams, int Id)
        {
            var result = await _IVatTuRepository.ThongKeVatTuThanhLyByMaVT(pagingParams, Id);
            Response.AddPagination(result.pl.CurrentPage, result.pl.PageSize, result.pl.TotalCount, result.pl.TotalPages);
            return Ok(
                new
                {
                    result.pl.Items,
                    result.tongluong            
                });
        }
        [HttpGet("ThongKeVatTuTonKhoByMaVT")]
        public async Task<IActionResult> ThongKeVatTuTonKhoByMaVT([FromQuery]PagingParams pagingParams, int Id)
        {
            var result = await _IVatTuRepository.ThongKeVatTuTonKhoByMaVT(pagingParams, Id);
            Response.AddPagination(result.pl.CurrentPage, result.pl.PageSize, result.pl.TotalCount, result.pl.TotalPages);
            return Ok(
               new
               {
                   result.pl.Items,
                   result.tongluong
               });
        }

        [HttpGet("getTongCong")]
        public async Task<IActionResult> GetTongCong([FromQuery]TotalWholeParams wholeParams)
        {
            var result = await _IVatTuRepository.GetTongCong(wholeParams);
            return Ok(result);
        }
    }
}