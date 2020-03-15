using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Order
    {
        public long ID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
