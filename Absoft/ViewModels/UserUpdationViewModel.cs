using Absoft.Helpers;
using System;

namespace Absoft.ViewModels
{
    public class UserUpdationViewModel
    {
        public Guid Id { get; set; }
        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value.ToLower().Trim();
            }
        }
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
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value.Trim();
            }
        }
        private string address;
        public string Address
        {
            get => address;
            set
            {
                address = value.ToTrim();
            }
        }
    }
}
