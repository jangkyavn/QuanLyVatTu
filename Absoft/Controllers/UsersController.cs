using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Absoft.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPaging([FromQuery]UserParams userParams)
        {
            var usersPaging = await _userRepository.GetAllPagingAsync(userParams);

            Response.AddPagination(usersPaging.CurrentPage, usersPaging.PageSize, usersPaging.TotalCount, usersPaging.TotalPages, usersPaging.FirstRowOnPage, usersPaging.LastRowOnPage);

            return Ok(usersPaging.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await _userRepository.GetByIdAsync(id.Value);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserCreationViewModel userViewModel)
        {
            var result = await _userRepository.AddAsync(userViewModel);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserUpdationViewModel userViewModel)
        {
            var result = await _userRepository.UpdateAsync(userViewModel);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut("changeStatus/{id}")]
        public async Task<IActionResult> ChangeStatus(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var result = await _userRepository.ChangeStatusAsync(id.Value);

            if (result)
            {
                return Ok(true);
            }

            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var result = await _userRepository.DeleteAsync(id.Value);

            if (result)
            {
                return Ok(true);
            }

            return NotFound();
        }
    }
}
