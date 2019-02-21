using System;
using System.Collections.Generic;

namespace Absoft.ViewModels
{
    public class UserListViewModel
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool Status { get; set; }
        public List<string> Roles { get; set; }
    }
}
