using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<NguoiDung> _userManager;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserRepository(
            UserManager<NguoiDung> userManager,
            DataContext dataContext,
            IMapper mapper)
        {
            _userManager = userManager;
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(UserCreationViewModel userViewModel)
        {
            var userModel = _mapper.Map<NguoiDung>(userViewModel);
            userModel.CreatedDate = DateTime.Now;

            var result = await _userManager.CreateAsync(userModel, userViewModel.Password);

            return result.Succeeded;
        }

        public async Task<bool> ChangeStatusAsync(Guid? id)
        {
            var userModel = await _userManager.FindByIdAsync(id.ToString());
            userModel.Status = !userModel.Status;
            var result = await _userManager.UpdateAsync(userModel);
            return result.Succeeded;
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            var model = await _userManager.FindByEmailAsync(email);
            return model != null;
        }

        public async Task<bool> CheckUserNameExistsAsync(string userName)
        {
            var model = await _userManager.FindByNameAsync(userName);
            return model != null;
        }

        public async Task<bool> DeleteAsync(Guid? id)
        {
            var userModel = await _userManager.FindByIdAsync(id.ToString());

            var result = await _userManager.DeleteAsync(userModel);

            return result.Succeeded;
        }

        public async Task<PagedList<UserListViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var usersQuery = from user in _dataContext.Users
                             orderby user.UserName
                             select new UserListViewModel
                             {
                                 Id = user.Id,
                                 UserName = user.UserName,
                                 Email = user.Email,
                                 FullName = user.FullName,
                                 Status = user.Status,
                                 Roles = (from userRole in user.UserRoles
                                          join role in _dataContext.Roles
                                          on userRole.RoleId equals role.Id
                                          select role.Name).ToList(),
                             };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                usersQuery = usersQuery.Where(x =>
                    x.UserName.ToUpper().Contains(keyword) ||
                    x.FullName.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    x.FullName.ToUpper().Contains(keyword) ||
                    x.Email.ToUpper().Contains(keyword));
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "userName":
                        if (pagingParams.SortValue == "ascend")
                        {
                            usersQuery = usersQuery.OrderBy(x => x.UserName);
                        }
                        else
                        {
                            usersQuery = usersQuery.OrderByDescending(x => x.UserName);
                        }
                        break;
                    case "fullName":
                        if (pagingParams.SortValue == "ascend")
                        {
                            usersQuery = usersQuery.OrderBy(x => x.FullName);
                        }
                        else
                        {
                            usersQuery = usersQuery.OrderByDescending(x => x.FullName);
                        }
                        break;
                    case "email":
                        if (pagingParams.SortValue == "ascend")
                        {
                            usersQuery = usersQuery.OrderBy(x => x.Email);
                        }
                        else
                        {
                            usersQuery = usersQuery.OrderByDescending(x => x.Email);
                        }
                        break;
                    default:
                        break;
                }
            }

            return await PagedList<UserListViewModel>.CreateAsync(usersQuery, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<UserDetailViewModel> GetByIdAsync(Guid? id)
        {
            var userModel = await _userManager.FindByIdAsync(id.ToString());
            var userViewModel = _mapper.Map<UserDetailViewModel>(userModel);
            return userViewModel;
        }

        public async Task<UserDetailViewModel> GetByUserNameAsync(string userName)
        {
            var userModel = await _userManager.FindByNameAsync(userName);
            var userViewModel = _mapper.Map<UserDetailViewModel>(userModel);
            return userViewModel;
        }

        public async Task<int> GetTotalCountAsync()
        {
            var count = await _userManager.Users.CountAsync();
            return count;
        }

        public async Task<bool> UpdateAsync(UserUpdationViewModel userViewModel)
        {
            var userModel = await _userManager.FindByNameAsync(userViewModel.UserName);
            _dataContext.Entry(userModel).CurrentValues.SetValues(userViewModel);
            var result = await _userManager.UpdateAsync(userModel);
            return result.Succeeded;
        }
    }
}
