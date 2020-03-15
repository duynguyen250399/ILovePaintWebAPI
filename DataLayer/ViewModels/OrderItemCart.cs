using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels
{
    public class OrderItemCart
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }   
        public int Quantity { get; set; }
        public float Amount { get; set; }
        public string Image { get; set; }
    }
}
