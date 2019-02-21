using System;

namespace Absoft.ViewModels
{
    public class UserSessionViewModel
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
    }
}
