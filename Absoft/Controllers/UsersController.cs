using Absoft.Data.Entities;
using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Absoft.Controllers
{
    public class UsersController : BaseController
    {
        private readonly SignInManager<NguoiDung> _signInManager;
        private readonly UserManager<NguoiDung> _userManager;
        private readonly IUserRepository _userRepository;

        public UsersController(
            SignInManager<NguoiDung> signInManager,
            UserManager<NguoiDung> userManager,
            IUserRepository userRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _userRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
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

        [HttpGet("getTotalCount")]
        public async Task<IActionResult> GetTotalCount()
        {
            var result = await _userRepository.GetTotalCountAsync();
            return Ok(result);
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

        [AllowAnonymous]
        [HttpGet("checkUserNameExists/{userName}")]
        public async Task<IActionResult> CheckUserNameExists(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            var result = await _userRepository.CheckUserNameExistsAsync(userName);
            return Ok(result);
        }

        [HttpGet("checkEmailExists/{email}")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }

            var result = await _userRepository.CheckEmailExistsAsync(email);
            return Ok(result);
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

        [HttpGet("checkCurrentPassword/{password}")]
        public async Task<IActionResult> CheckCurrentPassword(string password)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return Ok(result.Succeeded);
        }

        [HttpPut("changePassword/{newPassword}")]
        public async Task<IActionResult> ChangePassword(string newPassword)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return NoContent();
            }
            return Ok(true);
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
