using DataLayer.Entities;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public class OrderModel
    {
        public Order Order { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
