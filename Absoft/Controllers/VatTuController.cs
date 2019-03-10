using Absoft.Authorization;
using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IAuthorizationService _authorizationService;

        public VatTuController(IVatTuRepository IVatTuRepository,
            IAuthorizationService authorizationService)
        {
            _IVatTuRepository = IVatTuRepository;
            _authorizationService = authorizationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "VAT_TU", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _IVatTuRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "VAT_TU", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var paged = await _IVatTuRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("getAllPagingWithTongTon")]
        public async Task<IActionResult> GetAllPagingWithTongTonAsync([FromQuery]PagingParams pagingParams)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "VAT_TU", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

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
            var auth = await _authorizationService.AuthorizeAsync(User, "VAT_TU", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _IVatTuRepository.GetByIdAsync(id);
            return Ok(models);
        }
        [HttpGet("getByMaLoaiVT/{maLoaiVT}")]
        public async Task<IActionResult> GetByMaLoaiVTAsync(int maLoaiVT)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "VAT_TU", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _IVatTuRepository.GetByMaLoaiVTAsync(maLoaiVT);
            return Ok(models);
        }
        [HttpGet("getByMaDV/{maDVT}")]
        public async Task<IActionResult> GetByMaDVAsync(int maDVT)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "VAT_TU", Operations.Read);
            if (auth.Succeeded == false)
                return Unauthorized();

            var models = await _IVatTuRepository.GetByMaDVAsync(maDVT);
            return Ok(models);
        }
        [HttpPut]
        public async Task<IActionResult> Update(VatTuViewModel VatTuViewModel)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "VAT_TU", Operations.Update);
            if (auth.Succeeded == false)
                return Unauthorized();

            var result = await _IVatTuRepository.UpdateAsync(VatTuViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(VatTuViewModel VatTuViewModel)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "VAT_TU", Operations.Create);
            if (auth.Succeeded == false)
                return Unauthorized();

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
            var auth = await _authorizationService.AuthorizeAsync(User, "VAT_TU", Operations.Delete);
            if (auth.Succeeded == false)
                return Unauthorized();

            var result = await _IVatTuRepository.DeleteAsync(id);
            return Ok(result);
        }
        [HttpDelete("DeleteAllAsync/{strIds}")]
        public async Task<IActionResult> DeleteAllAsync(string strIds)
        {
            var auth = await _authorizationService.AuthorizeAsync(User, "VAT_TU", Operations.Delete);
            if (auth.Succeeded == false)
                return Unauthorized();

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
            var authCreate = await _authorizationService.AuthorizeAsync(User, "VAT_TU", Operations.Create);
            var authUpdate = await _authorizationService.AuthorizeAsync(User, "VAT_TU", Operations.Update);
            if (authCreate.Succeeded == false || authUpdate.Succeeded == false)
                return Unauthorized();

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
            Response.AddPagination(result.plkho.CurrentPage, result.plkho.PageSize, result.plkho.TotalCount, result.plkho.TotalPages);
            return Ok(
               new
               {
                   result.plkho.Items,
                   result.tongluong
               });
        }
    }
}