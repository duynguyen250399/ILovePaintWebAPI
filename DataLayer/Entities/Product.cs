using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(300)]
        public string Description { get; set; }                   
        public string Image { get; set; }       
        public virtual IEnumerable<Color> Colors { get; set; }
        public Nullable<int> ProviderID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual Category Category { get; set; }
        public IEnumerable<ProductVolume> ProductVolumes { get; set; }
    }
}
