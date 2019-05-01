using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Absoft.Controllers
{
    public class ChucNangController : BaseController
    {
        private readonly IChucNangRepository _chucNangRepository;

        public ChucNangController(IChucNangRepository chucNangRepository)
        {
            _chucNangRepository = chucNangRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _chucNangRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _chucNangRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }
    }
}