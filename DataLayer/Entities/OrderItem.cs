using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class OrderItem
    {
        public long ID { get; set; }
        [Required]
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public float Amount { get; set; }
        public Nullable<long> OrderID { get; set; }
        public float UnitPrice { get; set; }
        public float VolumeValue { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }


    }
}
