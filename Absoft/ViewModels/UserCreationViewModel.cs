﻿using System;

namespace Absoft.ViewModels
{
    public class UserCreationViewModel
    {
        public UserCreationViewModel()
        {
            Status = false;
        }

        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
    }
}
