using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Shipper
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string PasswordHashed { get; set; }
        public string PasswordSalted { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public Nullable<double> Salary { get; set; }

    }
}
