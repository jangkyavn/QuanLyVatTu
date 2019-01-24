using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Absoft.Controllers
{   
    public class NhapVatTuController : BaseController
    {
        INhapVatTuRepository _INhapVatTuRepository;

        public NhapVatTuController(INhapVatTuRepository INhapVatTuRepository)
        {
            _INhapVatTuRepository = INhapVatTuRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Insert(NhapVatTuParams nhapVatTuParams)
        {
            var result = await _INhapVatTuRepository.InsertAsync(nhapVatTuParams.mnhapvattu, nhapVatTuParams.listnhapchitiet);
            return Ok(result);
        }
    }
}