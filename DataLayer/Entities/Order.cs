using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities
{
    public class Order
    {
        public long ID { get; set; }
        public string FullName { get; set; }
        /*
         false - Male
         true - Female
             */
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        /*
         0 - Watting
         1 - Packed
         2 - Shipping
         3 - Finished
             */
        public Nullable<int> Status { get; set; }
        public Nullable<bool> IsMember { get; set; }

        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
        public Nullable<int> ShipperID { get; set; }
        public virtual Shipper Shipper { get; set; }
    }
}
