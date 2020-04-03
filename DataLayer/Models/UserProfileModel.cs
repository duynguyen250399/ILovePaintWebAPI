using Microsoft.AspNetCore.Http;
using System;

namespace DataLayer.Models
{
    public class UserProfileModel
    {
        public IFormFile Avatar { get; set; }
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool Gender { get; set; }
    }
}
