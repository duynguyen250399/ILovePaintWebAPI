
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }     
        public string Address { get; set; }
        public string Image { get; set; }
        public Nullable<int> RewardPoints { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool Gender { get; set; }
    }
}
