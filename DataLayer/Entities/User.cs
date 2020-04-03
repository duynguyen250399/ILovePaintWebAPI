
using Microsoft.AspNetCore.Identity;
using System;

namespace DataLayer.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public Nullable<int> RewardPoints { get; set; }
        public bool Gender { get; set; }
    }
}
