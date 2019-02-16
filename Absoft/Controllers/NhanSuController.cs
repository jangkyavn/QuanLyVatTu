using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Absoft.Controllers
{
    public class NhanSuController : BaseController
    {
        INhanSuRepository _INhanSuRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public NhanSuController(INhanSuRepository INhanSuRepository, IHostingEnvironment hostingEnvironment)
        {
            _INhanSuRepository = INhanSuRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _INhanSuRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _INhanSuRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var models = await _INhanSuRepository.GetByIdAsync(id);
            return Ok(models);
        }

        [HttpPut]
        public async Task<IActionResult> Update(NhanSuViewModel NhanSuViewModel)
        {
            var result = await _INhanSuRepository.UpdateAsync(NhanSuViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(NhanSuViewModel NhanSuViewModel)
        {
            var id = await _INhanSuRepository.CheckTonTai(NhanSuViewModel.HoTen);
            if (id == -1)
            {
                var result = await _INhanSuRepository.InsertAsync(NhanSuViewModel);
                return Ok(result);
            }
            else
            {
                if (await _INhanSuRepository.GetStatus(id) == true)
                {
                    return Ok(-1); // đã tồn tại không cho thêm
                }
                else
                {
                    NhanSuViewModel.MaNS = id;
                    var result = await _INhanSuRepository.UpdateAsync(NhanSuViewModel);
                    var resultChange = _INhanSuRepository.ChangStatus(id);
                    return Ok(result);
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _INhanSuRepository.IsDelete(id);
            return Ok(result);
        }
        [HttpGet("loadCities")]
        public IActionResult LoadCities()
        {
            string path = _hostingEnvironment.WebRootPath + "\\json\\city.json";
            var cities = new List<CityParam>().Deserialize(path);
            return new OkObjectResult(cities.OrderBy(x => x.name));
        }
        [HttpGet("loadDistricts/{cityId}")]
        public IActionResult LoadDistricts(int? cityId)
        {
            string path = _hostingEnvironment.WebRootPath + "\\json\\district.json";
            var districts = new List<DistrictsParam>().Deserialize(path);

            if (cityId.HasValue)
            {
                districts = districts.Where(x => x.parent_code == cityId.Value).OrderBy(x => x.name).ToList();
            }
            return new OkObjectResult(districts);
        }
        [HttpGet("loadNations")]
        public IActionResult LoadNations()
        {
            string path = _hostingEnvironment.WebRootPath + "\\json\\nation.json";
            var cities = new List<NationsParam>().Deserialize(path);
            return new OkObjectResult(cities.OrderBy(x => x.name));
        }
    }
}