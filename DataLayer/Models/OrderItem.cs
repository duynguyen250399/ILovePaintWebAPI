using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Models
{
    public class OrderItem
    {
        public long ID { get; set; }
        [Required]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public int Quantity { get; set; }
        public float Amount { get; set; }
        [Required]
        public long OrderID { get; set; }

    }
}
