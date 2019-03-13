using Absoft.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Absoft.Controllers
{
    public class HanhDongController : BaseController
    {
        private readonly IHanhDongRepository _hanhDongRepository;

        public HanhDongController(IHanhDongRepository hanhDongRepository)
        {
            _hanhDongRepository = hanhDongRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _hanhDongRepository.GetAllAsync();
            return Ok(models);
        }
    }
}