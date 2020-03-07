using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Color
    {
        [Key]
        [StringLength(10)]
        public string ID { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Image { get; set; }
        public virtual Product Product { get; set; }
    }
}
