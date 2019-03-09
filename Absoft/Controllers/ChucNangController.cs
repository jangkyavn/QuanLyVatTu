using Absoft.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
    }
}