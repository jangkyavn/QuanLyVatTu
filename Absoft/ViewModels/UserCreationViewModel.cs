using Absoft.Helpers;
using System;

namespace Absoft.ViewModels
{
    public class UserCreationViewModel
    {
        public UserCreationViewModel()
        {
            Status = false;
        }

        public Guid? Id { get; set; }
        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value.ToLower().Trim();
            }
        }

        public string Password { get; set; }
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value.ToLower().Trim();
            }
        }
        private string fullName;
        public string FullName
        {
            get => fullName;
            set
            {
                fullName = value.ToTrim();
                fullName = fullName.ToTitleCase();
            }
        }
        public bool Gender;
        public DateTime DateOfBirth;
        public bool Status;
    }
}
