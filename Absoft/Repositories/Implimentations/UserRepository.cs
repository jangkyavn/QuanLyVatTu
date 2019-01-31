using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

            userModel.Gender = true;
            userModel.DateOfBirth = DateTime.Now;
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

        public async Task<bool> CheckUserNameExists(string userName)
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

        public async Task<PagedList<UserListViewModel>> GetAllPagingAsync(UserParams userParams)
        {
            var usersQuery = (from user in _dataContext.Users
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
                                           select role.Name).ToList()
                              });

            if (!string.IsNullOrEmpty(userParams.Keyword))
            {
                var keywordUnSign = TextHelper.ConvertToUnSign(userParams.Keyword).ToUpper();

                usersQuery = usersQuery.Where(x => x.UserName.ToUpper().Contains(keywordUnSign) ||
                    TextHelper.ConvertToUnSign(x.FullName.ToUpper()).Contains(keywordUnSign) ||
                    TextHelper.ConvertToUnSign(x.Email.ToUpper()).Contains(keywordUnSign));
            }

            if (!string.IsNullOrEmpty(userParams.SortValue) && !userParams.SortValue.Equals("null"))
            {
                switch (userParams.SortKey)
                {
                    case "userName":
                        if (userParams.SortValue == "ascend")
                        {
                            usersQuery = usersQuery.OrderBy(x => x.UserName);
                        }
                        else
                        {
                            usersQuery = usersQuery.OrderByDescending(x => x.UserName);
                        }
                        break;
                    case "fullName":
                        if (userParams.SortValue == "ascend")
                        {
                            usersQuery = usersQuery.OrderBy(x => x.FullName);
                        }
                        else
                        {
                            usersQuery = usersQuery.OrderByDescending(x => x.FullName);
                        }
                        break;
                    case "email":
                        if (userParams.SortValue == "ascend")
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

            return await PagedList<UserListViewModel>.CreateAsync(usersQuery, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<UserDetailViewModel> GetByIdAsync(Guid? id)
        {
            var userModel = await _userManager.FindByIdAsync(id.ToString());
            var userViewModel = _mapper.Map<UserDetailViewModel>(userModel);
            return userViewModel;
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
