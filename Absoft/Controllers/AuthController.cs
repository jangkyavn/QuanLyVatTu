using Absoft.Data.Entities;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Absoft.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly UserManager<NguoiDung> _userManager;
        private readonly SignInManager<NguoiDung> _signInManager;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthController(UserManager<NguoiDung> userManager,
            SignInManager<NguoiDung> signInManager,
            IRoleRepository roleRepository,
            IMapper mapper,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _config = config;
        }

        #region API
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user == null)
            {
                return Unauthorized();
            }

            if (!user.Status)
            {
                return Ok(new LoginResult(false, "Tài khoản đã bị khóa!"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password, false);

            if (result.Succeeded)
            {
                var userModel = await _userManager.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == loginViewModel.UserName.ToUpper());

                var userSession = _mapper.Map<UserSessionViewModel>(userModel);

                return Ok(new LoginResult(true, await GenerateJwtAsync(userModel), userSession));
            }

            return Ok(new LoginResult(false, "Tài khoản hoặc mật khẩu không đúng!"));
        }
        #endregion

        #region Private Methods
        private async Task<string> GenerateJwtAsync(NguoiDung user)
        {
            var claims = new List<Claim>
           {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
