using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class UserRegistrationModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Address { get; set; }
    }
}
