using Absoft.Helpers;
using Absoft.ViewModels;
using System;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDetailViewModel> GetByIdAsync(Guid? id);
        Task<UserDetailViewModel> GetByUserNameAsync(string userName);
        Task<PagedList<UserListViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<bool> AddAsync(UserCreationViewModel userViewModel);
        Task<bool> UpdateAsync(UserUpdationViewModel userViewModel);
        Task<bool> DeleteAsync(Guid? id);
        Task<bool> ChangeStatusAsync(Guid? id);
        Task<bool> CheckUserNameExistsAsync(string userName);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<int> GetTotalCountAsync();
    }
}
