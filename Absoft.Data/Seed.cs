using Absoft.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Absoft.Data
{
    public class Seed
    {
        private readonly UserManager<NguoiDung> _userManager;
        private readonly RoleManager<VaiTro> _roleManager;

        public Seed(UserManager<NguoiDung> userManager,
            RoleManager<VaiTro> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                var roles = new List<VaiTro>
                {
                    new VaiTro() { Name= "Member"},
                    new VaiTro() { Name= "Admin"},
                    new VaiTro() { Name= "Moderator"}
                };

                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }

                var adminUser = new NguoiDung
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    FullName = "Administrator",
                    Gender = true,
                    DateOfBirth = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Status = true
                };

                var result = _userManager.CreateAsync(adminUser, "password").Result;
                if (result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("Admin").Result;
                    _userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" }).Wait();
                }
            }
        }
    }
}
