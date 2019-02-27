using System;
using System.Linq;

namespace Absoft.ViewModels
{
    public class UserListViewModel
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool Status { get; set; }
        public IQueryable<string> Roles { get; set; }
    }
}
