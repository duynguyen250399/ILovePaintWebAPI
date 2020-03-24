using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class UpdateOrderModel
    {
        public int OrderID { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> ShipperID { get; set; }
    }
}
