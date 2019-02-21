using System;

namespace Absoft.ViewModels
{
    public class UserDetailViewModel
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public bool? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
    }
}
