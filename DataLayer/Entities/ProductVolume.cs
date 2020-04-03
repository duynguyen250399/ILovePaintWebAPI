using System;

namespace DataLayer.Entities
{
    public class ProductVolume
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public Nullable<int> Status { get; set; }
        public float VolumeValue { get; set; }
    }
}
