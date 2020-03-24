using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class OrderEmailModel
    {
        public Order Order { get; set; }
        public IEnumerable<OrderItemEmail> OrderItems { get; set; }
    }

    public class OrderItemEmail
    {
        public string ProductName { get; set; }
        public float VolumeValue { get; set; }
        public float UnitPrice { get; set; }  
        public int Quantity { get; set; }
        public float Amount { get; set; }       
    }
}
